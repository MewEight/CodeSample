using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : ManagerBase
{
    public GameObject portalPrefab;

    private GameObject _currentPortal;
    private bool _portalAlive = true;
    private float _portalTimer;

    private Transform _portalTransform;
    private Transform _playerTransform;

    public System.Action<Transform> onPortalAppeared;

    private PortalSettings _settings;

    protected override void OnInitializeManager()
    {
        _settings = mainManager.gameSettings.portalSettings;

        _currentPortal = GameObject.Instantiate(portalPrefab);
        _currentPortal.SetActive(false);

        _portalTransform = _currentPortal.transform;

        mainManager.levelManager.OnLevelStart += OnLevelStart;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!_portalAlive)
        {
            if (_portalTimer <= mainManager.levelManager.GameTime)
            {
                _portalAlive = true;
                SpawnPortal();
            }
        }
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (_portalAlive)
        {
            float sqrDist = (_portalTransform.position - _playerTransform.position).sqrMagnitude;
            float lerp = Mathf.Clamp01(1 - (sqrDist / _settings.sqrDistance));
            mainManager.cameraManager.UpdatePortalPriority(lerp * _settings.maxPriority);
        }
    }

    private void OnLevelStart()
    {
        _portalTimer = mainManager.levelManager.GameTime + _settings.portalOpeningInterval.GetRandom();
        _portalAlive = false;

        _playerTransform = mainManager.levelManager.Player.transform;
    }

    public void SpawnPortal()
    {
        _currentPortal.transform.position = _playerTransform.position + (Vector3)(Utility.GetRandomDir(true) * _settings.distanceFromPlayer);
        _currentPortal.SetActive(true);
        mainManager.cameraManager.AddPortalTarget(_currentPortal.transform);
        onPortalAppeared?.Invoke(_portalTransform);
    }

    public void EnterPortal()
    {

    }
}
