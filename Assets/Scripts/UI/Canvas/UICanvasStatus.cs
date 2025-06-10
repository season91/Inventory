using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [MainMenu 구성요소] 좌측 캐릭터 정보창
/// </summary>
public class UICanvasStatus : UIBase
{
    [SerializeField] private TextMeshProUGUI tmpAmountMood;
    [SerializeField] private TextMeshProUGUI tmpAmountWeight;
    [SerializeField] private TextMeshProUGUI tmpAmountStamina;
    [SerializeField] private TextMeshProUGUI tmpAmountFullness;
    [SerializeField] private TextMeshProUGUI tmpAmountSocial;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.parent.Find("Btn_Back").GetComponent<Button>();
        tmpAmountMood = transform.Find("Group_Status_Info/Group_Status/Group_Stat_Mood/Tmp_Amount").GetComponent<TextMeshProUGUI>();
        tmpAmountWeight = transform.Find("Group_Status_Info/Group_Status/Group_Stat_Weight/Tmp_Amount").GetComponent<TextMeshProUGUI>();
        tmpAmountStamina = transform.Find("Group_Status_Info/Group_Status/Group_Stat_Stamina/Tmp_Amount").GetComponent<TextMeshProUGUI>();
        tmpAmountFullness = transform.Find("Group_Status_Info/Group_Status/Group_Stat_Fullness/Tmp_Amount").GetComponent<TextMeshProUGUI>();
        tmpAmountSocial = transform.Find("Group_Status_Info/Group_Status/Group_Stat_Social/Tmp_Amount").GetComponent<TextMeshProUGUI>();        
    }

    public override void Open(bool showBackButton)
    {
        base.Open(showBackButton);
        SetPlayerData();
    }

    public void SetPlayerData()
    {
        PlayerData playerData = GameManager.Instance.player.data;

        tmpAmountMood.text = playerData.mood.ToString();
        tmpAmountWeight.text = playerData.weight.ToString("N1") + "kg";
        tmpAmountStamina.text = playerData.stamina.ToString();
        tmpAmountFullness.text = playerData.fullness.ToString();
        tmpAmountSocial.text = playerData.social.ToString();
    }
}
