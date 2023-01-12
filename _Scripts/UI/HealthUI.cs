using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : BaseBehaviour
{
    public Image fillImage;

    private float _curValue;
    private float _maxValue;

    protected override void OnInitialize()
    {
    }

    public void SetUpHealthBar(float maxValue)
    {
        _maxValue = maxValue;
        _curValue = _maxValue;

        RefreshFillBar();
    }

    public void RefreshFillBar()
    {
        fillImage.fillAmount = _curValue / _maxValue;
    }

    public void UpdateHealth(float newValue, bool newMax = false)
    {
        if (newMax)
        {
            _maxValue = newValue;
        }

        _curValue = Mathf.Clamp(newValue, 0, _maxValue);
        RefreshFillBar();
    }
}
