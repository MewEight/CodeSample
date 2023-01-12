using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatisticsEnum
{
    None = 0,
    Enemy_Killed = 1,
}

public class StatisticManager : ManagerBase
{
    private Dictionary<StatisticsEnum, float> _statTable = new Dictionary<StatisticsEnum, float>();

    protected override void OnInitializeManager()
    {
        _statTable.Clear();

        mainManager.levelManager.enemyManager.onEnemyDeathEvent += OnEnemyDeath;
    }

    private void OnEnemyDeath(EnemyBase obj)
    {
        IncrementStats(StatisticsEnum.Enemy_Killed, 1);
    }

    private void IncrementStats(StatisticsEnum stat, float value)
    {
        if (!_statTable.ContainsKey(stat))
        {
            _statTable.Add(stat, 0);
        }

        _statTable[stat] += value;
    }
}
