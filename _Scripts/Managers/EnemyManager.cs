using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : ManagerBase
{
    public List<EnemyBase> enemyPrefabs = new List<EnemyBase>();

#if UNITY_EDITOR
    [Header("Debug")]
    public bool isTesting = false;
    public EnemyType forceType = EnemyType.BASIC;
#endif

    private float _distanceSqr;
    private float _checkTimer = 0;

    private EnemySettings _eSettings;
    private EnemyDataSettings _enemyData;

    private List<EnemyBase> _enemyList = new List<EnemyBase>();

    private Dictionary<EnemyType, List<EnemyBase>> _enemyPool = new Dictionary<EnemyType, List<EnemyBase>>();

    private Transform _enemyHolder;

    public System.Action<EnemyBase> onEnemyDeathEvent;

    protected override void OnInitializeManager()
    {
        _eSettings = mainManager.gameSettings.enemySettings;
        _enemyData = _eSettings.enemyData;

        _distanceSqr = _eSettings.maxDistanceFromPlayer * _eSettings.maxDistanceFromPlayer;
        _checkTimer = _eSettings.checkInterval;

        _enemyHolder = new GameObject("EnemyHolder").transform;
        _enemyHolder.transform.position = Vector3.zero;

        _enemyData.InitTable();
    }

    protected override void OnUpdate(float deltaTime)
    {
        _checkTimer -= deltaTime;
        if (_checkTimer <= 0)
        {
            _checkTimer = _eSettings.checkInterval;
            CheckEnemyInRange();
        }

        UpdateEnemies(deltaTime);
    }

    private void UpdateEnemies(float deltaTime)
    {
        int enemyCount = _enemyList.Count;
        for (int i = 0; i < enemyCount; i++)
        {
            _enemyList[i].UpdateBehaviour(deltaTime);
        }
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        int enemyCount = _enemyList.Count;
        for (int i = 0; i < enemyCount; i++)
        {
            _enemyList[i].FixedUpdateBehaviour(fixedDeltaTime);
        }
    }

    public void SpawnEnemies()
    {
        Vector2 playerSpawn = mainManager.levelManager.Player.transform.position;
        for (int i = 0; i < _eSettings.maxEnemiesAroundPlayer; i++)
        {
            EnemyBase newEnemy = GetEnemy(enemyPrefabs.GetRandom());
            newEnemy.transform.position = playerSpawn + (Random.insideUnitCircle.normalized * _eSettings.initialSpawnRadius);
            _enemyList.Add(newEnemy);
        }
    }

    private void CheckEnemyInRange()
    {
        int enemyToSpawn = _eSettings.maxEnemiesAroundPlayer - _enemyList.Count;
        Vector3 playerPos = mainManager.levelManager.Player.transform.position;
        for(int i = 0; i < _enemyList.Count; i++)
        {
            bool tooFar = (_enemyList[i].transform.position - playerPos).sqrMagnitude > _distanceSqr;
            if (tooFar)
            {
                EnemyBase enemy = _enemyList[i];
                PoolEnemy(enemy);
                enemyToSpawn++;
            }
        }

        Vector3 headingDir = mainManager.levelManager.Player.headingDir;
        for (int i = 0; i < enemyToSpawn; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(Random.Range(-45f, 45f), Vector3.forward) * headingDir;
            EnemyBase newEnemy = GetEnemy(enemyPrefabs.GetRandom());
            newEnemy.transform.position = playerPos + (dir * _eSettings.minSpawnRadius);
            _enemyList.Add(newEnemy);
        }
    }

    public void KillEnemy(EnemyBase enemy)
    {
        onEnemyDeathEvent?.Invoke(enemy);
        mainManager.levelManager.playerController.GivePlayerHealth(enemy.Energy);
        PoolEnemy(enemy);
    }

    public EnemyData GetEnemyData(EnemyType eType)
    {
        return _enemyData.GetEnemyData(eType);
    }

    #region
    public void PoolEnemy(EnemyBase enemy)
    {
        _enemyList.Remove(enemy);

        if (_enemyPool.ContainsKey(enemy.enemyType))
        {
            enemy.gameObject.SetActive(false);
            _enemyPool[enemy.enemyType].Add(enemy);
        }
        else
        {
            Destroy(enemy);
        }
    }

    public EnemyBase GetEnemy(EnemyBase enemy)
    {
        EnemyType eType = enemy.enemyType;
#if UNITY_EDITOR
        if (isTesting)
        {
            for (int i = 0; i < enemyPrefabs.Count; i++)
            {
                if (enemyPrefabs[i].enemyType == forceType)
                {
                    enemy = enemyPrefabs[i];
                    break;
                }
            }
        }
#endif
        if (_enemyPool.ContainsKey(eType))
        {
            if (_enemyPool[eType].Count > 0)
            {
                EnemyBase newEnemy = _enemyPool[eType][0];
                _enemyPool[eType].RemoveAt(0);
                newEnemy.gameObject.SetActive(true);
                return newEnemy;
            }
            else
            {
                return CreateNewEnemy(enemy);
            }
        }
        else
        {
            _enemyPool.Add(eType, new List<EnemyBase>());
            return CreateNewEnemy(enemy);
        }
    }

    private EnemyBase CreateNewEnemy(EnemyBase enemy)
    {
        EnemyBase newEnemy = Instantiate(enemy, _enemyHolder);
        newEnemy.Initialize(mainManager);
        newEnemy.SetManager(this);
        return newEnemy;
    }
    #endregion
}
