using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : ManagerBase
{
    public UpgradeSettings upgradeSettings;

    protected override void OnInitializeManager()
    {
        upgradeSettings.InitTable();
    }

}
