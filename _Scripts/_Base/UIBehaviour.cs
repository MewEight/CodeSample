using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBehaviour : BaseBehaviour
{
    /// <summary>
    /// Use rectTransform instead.
    /// </summary>
    private RectTransform _rectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            return _rectTransform;
        }
    }
}
