using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIJellyInfo : UIBase
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI tmpJelly;

    private void Reset()
    {
        imgIcon = GetComponentInChildren<Image>();
        tmpJelly = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Initialization()
    {
        base.Initialization();
        tmpJelly.text = $"{GameManager.Instance.player.data.jelly:N0}";
    }

}
