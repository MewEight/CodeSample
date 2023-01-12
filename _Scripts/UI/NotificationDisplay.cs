using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationDisplay : UIBehaviour
{
    public TextMeshProUGUI textDisplay;

    protected override void OnInitialize()
    {
    }

    public void SetUpDisplay(string text)
    {
        transform.localScale = Vector3.one;
        textDisplay.text = text;
    }
}
