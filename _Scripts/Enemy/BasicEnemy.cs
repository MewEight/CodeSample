using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    public override EnemyType enemyType => EnemyType.BASIC;

    protected override void OnHitPlayer(PlayerInstance player)
    {
        EnemyDeath();
    }
}
