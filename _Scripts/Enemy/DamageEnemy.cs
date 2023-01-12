using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damages the player instead of giving him health
/// </summary>
public class DamageEnemy : EnemyBase
{
    public override EnemyType enemyType => EnemyType.DAMAGE_ENEMY;

    protected override void OnHitPlayer(PlayerInstance player)
    {
        EnemyDeath();
    }
}
