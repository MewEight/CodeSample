using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase : BaseBehaviour
{
    protected override void OnInitialize()
    {
        OnInitializeManager();
    }

    protected abstract void OnInitializeManager();
}
