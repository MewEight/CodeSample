using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BASIC = 0,
    UNKILLABLE = 1,
    GOLD_ENEMY = 2,
    DAMAGE_ENEMY = 3,
    BOUNCE = 4,
}

public abstract class EnemyBase : GameEntity
{
    public abstract EnemyType enemyType { get; }

    [Header("Reference")]
    public Rigidbody2D myRigidBody;

    protected float _enemyEnergy = 5;
    protected int _enemyScore = 0;
    protected float _moveSpeed = 0;

    public float Energy => _enemyEnergy;
    public int Score => _enemyScore;
    public float MoveSpeed => _moveSpeed;

    protected EnemyManager _enemyManager;

    protected override void OnInitialize()
    {
        EnemyData data = mainManager.levelManager.enemyManager.GetEnemyData(enemyType);
        _enemyEnergy = data.energy;
        _enemyScore = data.score;
        _moveSpeed = data.moveSpeed;
    }

    public void SetManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerID.Player)
        {
            OnCollideWithPlayer(mainManager.levelManager.Player);
        }
    }

    protected void OnCollideWithPlayer(PlayerInstance player)
    {
        OnHitPlayer(player);
    }

    protected virtual void EnemyDeath()
    {
        _enemyManager.KillEnemy(this);
    }

    protected abstract void OnHitPlayer(PlayerInstance player);
}
