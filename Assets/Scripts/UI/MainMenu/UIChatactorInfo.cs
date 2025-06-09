using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChatactorInfo : UIBase
{
    private IGUI iguiImplementation;
    // 좌측 구성요소
    [SerializeField] private TextMeshProUGUI tmpName;
    [SerializeField] private TextMeshProUGUI tmpDescription;
    [SerializeField] private TextMeshProUGUI tmpAge;
    // [SerializeField] private Image imgExpBar;

    [SerializeField] private RectTransform fillBar;
    [SerializeField] private float maxGaugeWidth;
    // [SerializeField] private GameObject rightCap;
    private void Reset()
    {
        tmpName = GetComponentsInChildren<Transform>(true)
                  .FirstOrDefault(t => t.name == "Tmp_Name")?.GetComponent<TextMeshProUGUI>();
        tmpDescription = GetComponentsInChildren<Transform>(true)
                         .FirstOrDefault(t => t.name == "Tmp_Description")?.GetComponent<TextMeshProUGUI>();
        tmpAge = GetComponentsInChildren<Transform>(true)
                 .FirstOrDefault(t => t.name == "Tmp_Age")?.GetComponent<TextMeshProUGUI>();
        fillBar = GetComponentsInChildren<Transform>(true)
                  .FirstOrDefault(t => t.name == "Img_ExpBar")?.GetComponent<RectTransform>();
        maxGaugeWidth = (GetComponentsInChildren<Transform>(true)
                         .FirstOrDefault(t => t.name == "Img_ExpBar")
                         ?.GetComponent<RectTransform>()?.sizeDelta.x) ?? 0f;
    }

    public override void Open()
    {
        base.Open();
        SetPlayerData();
    }

    public void SetPlayerData()
    {
        PlayerData playerData = GameManager.Instance.player.data;
        tmpName.text = playerData.userName;
        tmpDescription.text = playerData.introduction;
        tmpAge.text = "Age:" +playerData.GetAge().ToString("N0") + "살 ";
        UpdateGauge((float) 4 / 12);
    }
    
    public void UpdateGauge(float ratio)
    {
        // ratio 값이 0보다 작거나 1보다 크지 않도록 안전하게 제한
        ratio = Mathf.Clamp01(ratio);

        // fillBar의 가로 길이를 statRatio * maxWidth만큼 설정
        fillBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ratio * maxGaugeWidth);
    }
}
