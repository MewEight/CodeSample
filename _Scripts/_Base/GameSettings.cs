using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public LevelSettings levelSettings;
    public EnemySettings enemySettings;
    public BackgroundSettings backgroundSettings;
    public PlayerSettings playerSettings;
    public PortalSettings portalSettings;
}

[System.Serializable]
public class LevelSettings
{
    public float slowTimeMultiplier = 0.5f;
}

[System.Serializable]
public class EnemySettings
{
    public float initialSpawnRadius = 5;
    public float minSpawnRadius = 10f;
    public int maxEnemiesAroundPlayer = 15;
    public float maxDistanceFromPlayer = 20f;
    public float checkInterval = 1;

    public EnemyDataSettings enemyData;
}

[System.Serializable]
public class BackgroundSettings
{
    public float scrollSpeed = 0.2f;
}

[System.Serializable]
public class PlayerSettings
{
    public float playerSpeed = 10;
    public float playerHealth = 100;
}

[System.Serializable]
public class PortalSettings
{
    public float sqrDistance = 50;
    public MinMaxFloat portalOpeningInterval = new MinMaxFloat(20, 50);
    public float portalOpenDuration = 10f;
    public float maxPriority = 1.5f;
    public float distanceFromPlayer = 20f;
}