using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasMenuBtn : UIBase
{
    // 우측 버튼
    [SerializeField] private Button btnStatus;
    [SerializeField] private Button btnInventory;

    // 버튼 처리 델리게이트 선언
    public delegate void MenuButtonClickHandler(MainButtonType type);
    
    // OnMenuButtonClicked 이벤트는 UICanvasMainMenu에서 넣어줄 것
    public event MenuButtonClickHandler OnMenuButtonClicked;
    
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.parent.Find("Btn_Back").GetComponent<Button>();
        btnStatus = transform.Find("Btn_Status").GetComponent<Button>();
        btnInventory = transform.Find("Btn_Inventory").GetComponent<Button>();
    }

    public override void Initialization()
    {
        base.Initialization();
        btnStatus.onClick.RemoveAllListeners();
        btnInventory.onClick.RemoveAllListeners();
        btnBack.onClick.RemoveAllListeners();

        btnStatus.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Status));
        btnInventory.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Inventory));
        btnBack.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Back));
    }
}
