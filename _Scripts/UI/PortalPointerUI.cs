using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPointerUI : UIBehaviour
{
    public Transform portalPointer;

    private Transform _playerTransform;
    private Transform _portalTransform;
    private bool _isActive = false;

    private Vector3 _forward = Vector3.forward;

    protected override void OnInitialize()
    {
        _isActive = false;
        portalPointer.SetActive(false);

        mainManager.levelManager.portalManager.onPortalAppeared += OnPortalAppear;
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (_isActive)
        {
            Vector3 dirToPortal = _portalTransform.position - _playerTransform.position;
            dirToPortal.Normalize();

            portalPointer.eulerAngles = _forward * -(Mathf.Atan2(dirToPortal.x, dirToPortal.y) * Mathf.Rad2Deg);

            Vector3 viewPos = mainManager.cameraManager.MainCam.WorldToViewportPoint(_portalTransform.position);
            viewPos.x = Mathf.Clamp(viewPos.x, 0.1f, 0.9f);
            viewPos.y = Mathf.Clamp(viewPos.y, 0.1f, 0.9f);

            portalPointer.position = mainManager.cameraManager.MainCam.ViewportToScreenPoint(viewPos);
        }
    }

    private void OnPortalAppear(Transform portal)
    {
        portalPointer.SetActive(true);
        _playerTransform = mainManager.levelManager.Player.transform;
        _portalTransform = portal;
        _isActive = true;
    }
}
