using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [Space]
    public Camera mainCam;

    private bool _fullyInitialized = false;
    private float _deltaTime = 0;
    private float _fixedDeltaTime = 0;

    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Managers")]
    public LevelManager levelManager;
    public UIManager uiManager;
    public CameraManager cameraManager;
    public BackgroundManager backgroundManager;
    public UpgradesManager upgradeManager;

    [Header("Debug")]
    public bool quickStart = true;

    // Start is called before the first frame update
    void Start()
    {
        levelManager.Initialize(this);
        uiManager.Initialize(this);
        cameraManager.Initialize(this);
        backgroundManager.Initialize(this);
        upgradeManager.Initialize(this);

        _fullyInitialized = true;

#if UNITY_EDITOR
        if (quickStart)
        {
            levelManager.QuickDevStart();
        }
#endif
    }

    private void Update()
    {
        if (!_fullyInitialized)
        {
            return;
        }

        _deltaTime = Time.deltaTime;

        levelManager.UpdateBehaviour(_deltaTime);
        uiManager.UpdateBehaviour(_deltaTime);
        cameraManager.UpdateBehaviour(_deltaTime);
        backgroundManager.UpdateBehaviour(_deltaTime);
        upgradeManager.UpdateBehaviour(_deltaTime);
    }

    private void FixedUpdate()
    {
        if (!_fullyInitialized)
        {
            return;
        }

        _fixedDeltaTime = Time.fixedDeltaTime;

        levelManager.FixedUpdateBehaviour(_fixedDeltaTime);
        uiManager.FixedUpdateBehaviour(_fixedDeltaTime);
        cameraManager.FixedUpdateBehaviour(_fixedDeltaTime);
        backgroundManager.FixedUpdateBehaviour(_fixedDeltaTime);
        upgradeManager.FixedUpdateBehaviour(_fixedDeltaTime);
    }
}
