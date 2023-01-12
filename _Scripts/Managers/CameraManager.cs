using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : ManagerBase
{
    public Cinemachine.CinemachineVirtualCamera virtualCam;
    public Cinemachine.CinemachineTargetGroup targetGroup;

    private Camera _mainCam;

    public Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }
    }

    protected override void OnInitializeManager()
    {
    }

    public void AddPlayerTarget(Transform followTarget)
    {
        targetGroup.AddMember(followTarget, 1, 1);
        //virtualCam.Follow = followTarget;
    }

    public void AddPortalTarget(Transform portal)
    {
        if (targetGroup.FindMember(portal) <= 0)
        {
            targetGroup.AddMember(portal, 0, 2);
        }
    }

    public void RemovePortalTarget(Transform portal)
    {
        targetGroup.RemoveMember(portal);
    }

    public void UpdatePortalPriority(float priority)
    {
        targetGroup.m_Targets[1].weight = priority;
    }
}
