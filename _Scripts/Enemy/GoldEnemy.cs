using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEnemy : EnemyBase
{
    public MinMaxFloat changeDirectionInterval = new MinMaxFloat(1f, 4f);

    private float _nextSwitchTimer = 0;
    private Vector2 _headingDir = Vector2.zero;

    public override EnemyType enemyType => EnemyType.GOLD_ENEMY;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        SwitchDirection();
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (_nextSwitchTimer < mainManager.levelManager.GameTime)
        {
            SwitchDirection();
        }
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        base.OnFixedUpdate(fixedDeltaTime);
        myRigidBody.MovePosition(myRigidBody.position + (_headingDir * _moveSpeed * fixedDeltaTime));
    }

    private void SwitchDirection()
    {
        _nextSwitchTimer = mainManager.levelManager.GameTime + changeDirectionInterval.GetRandom();
        _headingDir = Utility.GetRandomDir(true);
    }

    protected override void OnHitPlayer(PlayerInstance player)
    {
        EnemyDeath();
    }
}
