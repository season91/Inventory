using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharactorInfo : UIBase
{
    // 좌측 구성요소
    [SerializeField] private TextMeshProUGUI tmpName;
    [SerializeField] private TextMeshProUGUI tmpDescription;
    [SerializeField] private TextMeshProUGUI tmpAge;

    [SerializeField] private RectTransform fillBar;
    [SerializeField] private float maxGaugeWidth;
    
    private Sequence typingSequence;
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.parent.Find("Btn_Back").GetComponent<Button>();
        
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
        tmpAge.text = "Age:" +playerData.GetAge().ToString("N0") + "살 ";
        StartTypingEffect();
        UpdateGauge((float) 4 / 12);
    }
    
    public void UpdateGauge(float ratio)
    {
        // ratio 값이 0보다 작거나 1보다 크지 않도록 안전하게 제한
        ratio = Mathf.Clamp01(ratio);

        // fillBar의 가로 길이를 statRatio * maxWidth만큼 설정
        fillBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ratio * maxGaugeWidth);
    }

    private void StartTypingEffect()
    {
        if (typingSequence != null && typingSequence.IsActive()) typingSequence.Kill();

        string[] lines = GameManager.Instance.player.data.introduction.Split('\n');
        tmpDescription.text = "";

        typingSequence = DOTween.Sequence();

        foreach (var line in lines)
        {
            typingSequence.AppendCallback(() => tmpDescription.text += line + "\n");
            typingSequence.AppendInterval(0.6f);
        }

        typingSequence.AppendInterval(1.5f);
        typingSequence.Append(tmpDescription.DOFade(0, 0.8f));
        typingSequence.AppendCallback(() =>
        {
            tmpDescription.text = "";
            tmpDescription.alpha = 1f;
        });
        typingSequence.AppendInterval(0.5f);
        typingSequence.AppendCallback(StartTypingEffect);
    }
}
