using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITooltip : UIBase
{
    [SerializeField] private TextMeshProUGUI tmpDescription;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform tooltipRect;
    private Vector2 offset = new Vector2(-250f, 0f); 
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.parent.Find("Btn_Back").GetComponent<Button>();
        tmpDescription = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDescription(string description)
    {
        tmpDescription.text = description;
    }

    // slot에서 델리게이트 실행할 때 넣어줄거임
    public void SetPosition(Transform slotTransform)
    {
        
        Vector2 worldPos = slotTransform.position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            worldPos,
            null, // Screen Space - Overlay 캔버스일 경우
            out Vector2 anchoredPos
        );

        // 툴팁 UI의 위치 설정
        tooltipRect.anchoredPosition = anchoredPos + offset;
    }
}
