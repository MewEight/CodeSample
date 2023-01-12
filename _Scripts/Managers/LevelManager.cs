using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MAIN_MENU,
    PLAYING,
    PAUSED,
    GAME_OVER,
}

public class LevelManager : ManagerBase
{
    public GameState curState = GameState.MAIN_MENU;

    public PlayerController playerController;
    public EnemyManager enemyManager;
    public PortalManager portalManager;

    public System.Action OnLevelStart;
    public PlayerInstance Player => playerController.pInstance;

    private float _timeMultiplier = 1;
    public float TimeMultiplier
    {
        get { return _timeMultiplier; }
        private set { _timeMultiplier = value; }
    }

    private LevelSettings _settings;

    private float _gameTime = 0;
    public float GameTime => _gameTime;

    protected override void OnInitializeManager()
    {
        curState = GameState.MAIN_MENU;

        playerController.Initialize(mainManager);
        enemyManager.Initialize(mainManager);
        portalManager.Initialize(mainManager);

        _settings = mainManager.gameSettings.levelSettings;

        mainManager.uiManager.ingameUI.joystick.onPlayerTouch += OnPlayerTouch;
        mainManager.uiManager.ingameUI.joystick.onPlayerRelease += OnPlayerRelease;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (curState == GameState.PLAYING)
        {
            deltaTime *= TimeMultiplier;

            playerController.UpdateBehaviour(deltaTime);
            enemyManager.UpdateBehaviour(deltaTime);
            portalManager.UpdateBehaviour(deltaTime);
            _gameTime += deltaTime;
        }
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        if (curState == GameState.PLAYING)
        {
            fixedDeltaTime *= TimeMultiplier;
            playerController.FixedUpdateBehaviour(fixedDeltaTime);
            enemyManager.FixedUpdateBehaviour(fixedDeltaTime);
            portalManager.FixedUpdateBehaviour(fixedDeltaTime);
        }
    }

    private void StartGame()
    {
        curState = GameState.PLAYING;

        playerController.SpawnPlayer();
        enemyManager.SpawnEnemies();

        OnLevelStart?.Invoke();
    }

    public void SlowDownTime()
    {
        TimeMultiplier = _settings.slowTimeMultiplier;
    }

    public void RestoreTime()
    {
        TimeMultiplier = 1;
    }
    private void OnPlayerRelease()
    {
        RestoreTime();
    }

    private void OnPlayerTouch()
    {
        SlowDownTime();
    }

#if UNITY_EDITOR
    public void QuickDevStart()
    {
        StartGame();
    }
#endif 
}
