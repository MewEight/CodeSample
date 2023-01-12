using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "Data/UpgradeSettings")]
public class UpgradeSettings : ScriptableObject
{
    public List<BaseUpgrade> listOfUpgrades = new List<BaseUpgrade>();

    private Dictionary<UpgradeID, BaseUpgrade> _upgradeTable = new Dictionary<UpgradeID, BaseUpgrade>();

    public void InitTable()
    {
        for(int i = 0; i < listOfUpgrades.Count; i++)
        {
            if (!_upgradeTable.ContainsKey(listOfUpgrades[i].id))
            {
                _upgradeTable.Add(listOfUpgrades[i].id, listOfUpgrades[i]);
            }
            else
            {
                DevLog.LogError("Duplicate ID in upgrades: " + listOfUpgrades[i].id);
            }
        }
    }
}
