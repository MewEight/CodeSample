using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    protected MainManager mainManager;

    public void Initialize(MainManager manager)
    {
        mainManager = manager;
        OnInitialize();
    }

    public void UpdateBehaviour(float deltaTime)
    {
        OnUpdate(deltaTime);
    }

    public void FixedUpdateBehaviour(float fixedDeltaTime)
    {
        OnFixedUpdate(fixedDeltaTime);
    }

    protected abstract void OnInitialize();
    protected virtual void OnUpdate(float deltaTime) { }
    protected virtual void OnFixedUpdate(float fixedDeltaTime) { }
}
