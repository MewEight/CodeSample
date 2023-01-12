using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : BaseBehaviour
{
    public float yOffset;

    public HealthUI healthUI;
    public StatusEffectUI statusUI;

    private Vector3 upVec;
    private Transform _target;

    protected override void OnInitialize()
    {
        upVec = Vector3.up;

        healthUI.Initialize(mainManager);
        statusUI.Initialize(mainManager);
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (_target != null)
        {
            transform.position = mainManager.cameraManager.MainCam.WorldToScreenPoint(_target.position) + (upVec * yOffset);
        }
    }

    public void SetUpHud(GameEntity target)
    {
        _target = target.transform;

        if (target is PlayerInstance)
        {
            PlayerInstance pInstance = (PlayerInstance)target;
            healthUI.SetUpHealthBar(pInstance.MaxHealth);
        }
    }

    public void UpdateHealth(float newValue)
    {
        healthUI.UpdateHealth(newValue);
    }

    internal void ToggleStunState(bool state)
    {
        statusUI.ToggleStunStatus(state);
    }
}
