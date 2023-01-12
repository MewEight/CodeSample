using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : ManagerBase
{
    public SpriteRenderer backgroundSprite;

    private Material _cachedMat;
    private Vector2 _normalizedDir;
    private Vector2 _prevScroll = Vector2.zero;
    private int _mainTexID = 0;

    private BackgroundSettings _settings;

    protected override void OnInitializeManager()
    {
        _cachedMat = backgroundSprite.material;

        _mainTexID = Shader.PropertyToID("_MainTex");

        _settings = mainManager.gameSettings.backgroundSettings;
    }

    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        _prevScroll += _normalizedDir * fixedDeltaTime * _settings.scrollSpeed;
        _cachedMat.SetTextureOffset(_mainTexID, _prevScroll);
    }

    public void SetScroll(Vector3 dir)
    {
        _normalizedDir = dir;
    }
}
