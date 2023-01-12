using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    [Header("UIs")]
    public InGameUI ingameUI;

    [Header("References")]
    public Canvas myCanvas;

    protected override void OnInitializeManager()
    {
        ingameUI.Initialize(mainManager);
    }

    protected override void OnUpdate(float deltaTime)
    {
        ingameUI.UpdateBehaviour(deltaTime);
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        ingameUI.FixedUpdateBehaviour(fixedDeltaTime);
    }
}
