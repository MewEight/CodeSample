using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : EnemyBase
{
    public float stunDuration = 2f;

    public override EnemyType enemyType => EnemyType.BOUNCE;

    protected override void OnHitPlayer(PlayerInstance player)
    {
        Vector3 bounceDir = player.transform.position - transform.position;

        mainManager.levelManager.playerController.UpdatePlayerRotation(bounceDir, true);
        mainManager.levelManager.playerController.LaunchPlayer(bounceDir, true);
        mainManager.levelManager.playerController.StunPlayer(stunDuration);
        EnemyDeath();
    }
}
