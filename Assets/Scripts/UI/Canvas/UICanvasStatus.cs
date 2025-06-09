using TMPro;
using UnityEngine;
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
