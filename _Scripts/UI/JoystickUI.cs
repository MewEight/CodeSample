using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUI : BaseBehaviour
{
    public RectTransform joystickBase;
    public RectTransform joystickTop;
    public GameObject joystickHolder;

    [Header("Settings")]
    public float clampMag = 50;

    private Camera _mainCam;

    private Vector3 _startScreenPos;
    private Vector3 _inputStartPos;
    private Vector3 _curInputPos;
    private float _clampSqrMag;

    public System.Action onPlayerTouch;
    public System.Action onPlayerRelease;

    protected override void OnInitialize()
    {
        _mainCam = mainManager.mainCam;
        _clampSqrMag = clampMag * clampMag;

        joystickHolder.SetActive(false);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startScreenPos = Input.mousePosition;
            _inputStartPos = Input.mousePosition;//_mainCam.ScreenToWorldPoint(_startScreenPos);
            _curInputPos = _inputStartPos;
            joystickBase.position = _inputStartPos;
            joystickTop.position = _inputStartPos;
            joystickHolder.SetActive(true);
            onPlayerTouch?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 curScreenPos = Input.mousePosition;
            _curInputPos = curScreenPos;
            Vector3 direction = _inputStartPos - _curInputPos;
            mainManager.levelManager.playerController.UpdatePlayerRotation(direction);

            Vector3 screenDir = _startScreenPos - curScreenPos;

            if (screenDir.sqrMagnitude >= _clampSqrMag)
            {
                Vector3 maxPos = screenDir.normalized;
                maxPos *= clampMag;
                joystickTop.position = _startScreenPos - maxPos;
            }
            else
            {
                joystickTop.position = curScreenPos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _curInputPos = Input.mousePosition;
            if ((_inputStartPos - _curInputPos).sqrMagnitude > 0.5)
            {
                Vector3 direction = (_inputStartPos - _curInputPos).normalized;
                mainManager.levelManager.playerController.LaunchPlayer
                    (direction);
                mainManager.backgroundManager.SetScroll(direction);
            }
            joystickHolder.SetActive(false);
            onPlayerRelease?.Invoke();
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            mainManager.levelManager.playerController.StopPlayer();
        }
#endif
    }
}
