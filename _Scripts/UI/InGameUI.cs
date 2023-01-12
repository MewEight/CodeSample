using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUI : UIBehaviour
{
    public Transform hudHolder;

    [Space]
    public JoystickUI joystick;
    public GameNotificationUI notification;
    public PortalPointerUI portalUI;

    [Header("Text")]
    public TextMeshProUGUI scoreText;

    [Header("Prefabs")]
    public HudUI hudPrefab;

    private HudUI _playerHud;

    private Coroutine _scoreRoutine;

    private int _currentScore;

    protected override void OnInitialize()
    {
        mainManager.levelManager.playerController.onPlayerScoreChange += OnPlayerScore;

        joystick.Initialize(mainManager);
        notification.Initialize(mainManager);
        portalUI.Initialize(mainManager);

        ResetInGameUI();
    }

    public void ResetInGameUI()
    {
        _currentScore = 0;
        scoreText.text = "0";

        notification.ClearNotification();
    }

    private void OnPlayerScore(int newScore)
    {
        if (_scoreRoutine != null)
        {
            StopCoroutine(_scoreRoutine);
        }

        _scoreRoutine = StartCoroutine(_UpdateScoreRoutine(newScore));
    }

    private IEnumerator _UpdateScoreRoutine(int newScore)
    {
        int curScore = _currentScore;
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime;
            _currentScore = (int)Mathf.Lerp(curScore, newScore, lerp);
            scoreText.text = _currentScore.ToString();
            yield return null;
        }

        _currentScore = newScore;
        scoreText.text = _currentScore.ToString();
    }

    protected override void OnUpdate(float deltaTime)
    {
        joystick.UpdateBehaviour(deltaTime);
        notification.UpdateBehaviour(deltaTime);
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (_playerHud != null)
        {
            _playerHud.FixedUpdateBehaviour(fixedDeltaTime);
        }
        portalUI.FixedUpdateBehaviour(fixedDeltaTime);
    }

    public HudUI SetUpHud(GameEntity player)
    {
        _playerHud = Instantiate(hudPrefab, hudHolder);
        _playerHud.Initialize(mainManager);
        _playerHud.SetUpHud(player);

        return _playerHud;
    }
}
