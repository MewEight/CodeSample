using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNotificationUI : UIBehaviour
{
    public NotificationDisplay notificationPrefab;

    public Transform holder;

    private ObjectPool<NotificationDisplay> _notificationPool = new ObjectPool<NotificationDisplay>();

    private Vector3 _zeroVec = Vector3.zero;

    protected override void OnInitialize()
    {
        _notificationPool.originalPrefab = notificationPrefab;

        mainManager.levelManager.portalManager.onPortalAppeared += OnPortalAppeared;
    }

    private void OnPortalAppeared(Transform portal)
    {
        ShowPortalAppeared();
    }

    protected override void OnUpdate(float deltaTime)
    {
        for (int i = 0; i < _notificationPool.activePool.Count; i++)
        {
            _notificationPool.activePool[i].UpdateBehaviour(deltaTime);
        }
    }

    public void ShowPortalAppeared()
    {
        ShowText("A Portal Has Appeared!");
    }

    private void ShowText(string text)
    {
        NotificationDisplay display = _notificationPool.GetFromPool();
        display.transform.SetParent(holder);
        display.rectTransform.anchoredPosition = _zeroVec;
        display.SetUpDisplay(text);

        RepositionNotifications();
    }

    public void ClearNotification()
    {
        _notificationPool.ClearAllActive();
    }

    private void RepositionNotifications()
    {
        float yPos = -notificationPrefab.rectTransform.sizeDelta.y / 2;
        for (int i = 0; i < _notificationPool.activePool.Count; i++)
        {
            _notificationPool.activePool[i].rectTransform.SetYAnchorPos(yPos * (i + 1));
        }
    }
}
