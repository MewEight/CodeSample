using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSettings", menuName = "Data/EnemyDataSettings")]
public class EnemyDataSettings : ScriptableObject
{
    public List<EnemyData> enemyDataList = new List<EnemyData>();

    private Dictionary<EnemyType, EnemyData> _dataTable = new Dictionary<EnemyType, EnemyData>();

    public void InitTable()
    {
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            if (!_dataTable.ContainsKey(enemyDataList[i].type))
            {
                _dataTable.Add(enemyDataList[i].type, enemyDataList[i]);
            }
            else
            {
                DevLog.LogError("Duplicate Entry! : " + enemyDataList[i].type);
            }
        }
    }

    public EnemyData GetEnemyData(EnemyType type)
    {
        if (!_dataTable.ContainsKey(type))
        {
            DevLog.LogError("Missing Type Just Returning First Data");
            return _dataTable[0];
        }
        return _dataTable[type];
    }
}