using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusEffectUI : BaseBehaviour
{
    public TextMeshProUGUI statusText;

    protected override void OnInitialize()
    {
        statusText.text = "STUNNED";

        ToggleStunStatus(false);
    }

    public void ToggleStunStatus(bool state)
    {
        statusText.gameObject.SetActive(state);
    }
}
