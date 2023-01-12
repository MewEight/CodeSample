using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : GameEntity
{
    public Transform playerGraphic;

    private float _maxHealth;
    private float _health;
    private float _playerSpeed;

    private Rigidbody2D _myRigidBody;
    private Vector2 _normalizedDir;
    private HudUI _myHUD;

    public Vector2 headingDir => _normalizedDir;
    public float MaxHealth => _maxHealth;

    protected override void OnInitialize()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();

        SetUpPlayer();

        mainManager.cameraManager.AddPlayerTarget(transform);
        _myHUD = mainManager.uiManager.ingameUI.SetUpHud(this);
    }

    protected override void OnUpdate(float deltaTime)
    {
        _health -= deltaTime;
        _myHUD.UpdateHealth(_health);
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        _myRigidBody.MovePosition(_myRigidBody.position + (_normalizedDir * _playerSpeed * fixedDeltaTime));
    }

    public void LaunchPlayer(Vector3 direction)
    {
        if (_normalizedDir.SqrMagnitude() != 1)
        {
            _normalizedDir.Normalize();
        }
        _normalizedDir = direction;
    }

    public void UpdatePlayerRotation(Vector3 faceDir)
    {
        transform.eulerAngles = Vector3.forward * -(Mathf.Atan2(faceDir.x, faceDir.y) * Mathf.Rad2Deg);
    }

    public void StopPlayer()
    {
        _normalizedDir = Vector2.zero;
    }

    public void AddHealth(float amount)
    {
        _health += amount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void SetUpPlayer()
    {
        PlayerSettings settings = mainManager.gameSettings.playerSettings;

        _playerSpeed = settings.playerSpeed;
        _maxHealth = settings.playerHealth;
        _health = _maxHealth;
    }

    internal void Stun()
    {
        _myHUD.ToggleStunState(true);
    }

    internal void HideStunStatus()
    {
        _myHUD.ToggleStunState(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        EnemySettings enemySettings = GameObject.Find("_GameSettings")?.GetComponent<GameSettings>()?.enemySettings;
        if (enemySettings != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, enemySettings.minSpawnRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemySettings.maxDistanceFromPlayer);
        }
    }
#endif
}
