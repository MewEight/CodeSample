using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : GameEntity
{
    protected override void OnInitialize()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerID.Player)
        {
            mainManager.levelManager.portalManager.EnterPortal();
        }
    }
}
