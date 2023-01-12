using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : BaseBehaviour
{
    public PlayerInstance playerPrefab;

    private PlayerInstance _pInstance;

    public PlayerInstance pInstance => _pInstance;

    public System.Action<int> onPlayerScoreChange;

    private int _currentScore;

    private float _stunTimer = 0;
    private bool _isStunned = false;

    public int CurrentScore => _currentScore;

    protected override void OnInitialize()
    {
        mainManager.levelManager.enemyManager.onEnemyDeathEvent += OnEnemyDeath;

        _stunTimer = 0;
    }

    protected override void OnUpdate(float deltaTime)
    {
        _pInstance.UpdateBehaviour(deltaTime);

        if (_isStunned)
        {
            if (_stunTimer < mainManager.levelManager.GameTime)
            {
                _isStunned = false;
                _pInstance.HideStunStatus();
            }
        }
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        _pInstance.FixedUpdateBehaviour(fixedDeltaTime);
    }

    public void UpdatePlayerRotation(Vector3 facingDir, bool force = false)
    {
        if (_stunTimer > mainManager.levelManager.GameTime && !force)
            return;

        _pInstance.UpdatePlayerRotation(facingDir);
    }

    public void LaunchPlayer(Vector3 facingDir, bool force = false)
    {
        if (_stunTimer > mainManager.levelManager.GameTime && !force)
            return;

        _pInstance.LaunchPlayer(facingDir);
    }

    public void StopPlayer()
    {
        _pInstance.StopPlayer();
    }

    public void GivePlayerHealth(float health)
    {
        _pInstance.AddHealth(health);
    }

    public void SpawnPlayer()
    {
        _pInstance = GameObject.Instantiate(playerPrefab);
        _pInstance.Initialize(mainManager);
    }

    public void StunPlayer(float stunDuration)
    {
        _stunTimer = mainManager.levelManager.GameTime + stunDuration;
        _pInstance.Stun();
        _isStunned = true;
    }

    #region Events
    private void OnEnemyDeath(EnemyBase enemy)
    {
        _currentScore += enemy.Score;
        onPlayerScoreChange?.Invoke(_currentScore);
    }
    #endregion
}
