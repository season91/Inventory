using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 큰 틀이 되는 메인 메뉴로 구성요소는 총 3개
/// </summary>
public class UICanvasMainMenu : UIBase
{
    // 좌측 캐릭터 정보 창
    [SerializeField] private UICharactorInfo charactorInfo;
    
    // 우측 상단 창
    [SerializeField] private UIJellyInfo jellyInfo;
    
    // 우측 버튼 및 창
    [SerializeField] private UICanvasMenuBtn canvasMenuBtn;
    [SerializeField] private UICanvasInventory canvasInventory;
    [SerializeField] private UICanvasStatus canvasStatus;
    
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.Find("Btn_Back").GetComponent<Button>();
        
        charactorInfo = GetComponentInChildren<UICharactorInfo>();
        jellyInfo = GetComponentInChildren<UIJellyInfo>();
        
        canvasMenuBtn =  GetComponentInChildren<UICanvasMenuBtn>();
        canvasInventory = GetComponentInChildren<UICanvasInventory>();
        canvasStatus = GetComponentInChildren<UICanvasStatus>();
    }
    
    public override void Initialization()
    {
        base.Initialization();
        canvasMenuBtn.OnMenuButtonClicked += HandleMenuButtonClicked;
    }

    public override void Open()
    {
        base.Open();
        charactorInfo.Open();
        jellyInfo.Open();
        canvasMenuBtn.Open();
    }

    private void HandleMenuButtonClicked(MainButtonType type)
    {
        switch (type)
        {
            case MainButtonType.Status:
                ShowStatus();
                break;
            case MainButtonType.Inventory:
                ShowInventory();
                break;
            case MainButtonType.Back:
                ShowMainMenu();
                break;
        }
    }

    private void ShowStatus()
    {
        canvasMenuBtn.Close();
        canvasStatus.Open(true);
    }

    private void ShowInventory()
    {
        canvasMenuBtn.Close();
        canvasInventory.Open(true);
    }

    private void ShowMainMenu()
    {
        if(canvasStatus.gameObject.activeSelf) canvasStatus.Close();
        if(canvasInventory.gameObject.activeSelf) canvasInventory.Close();
        canvasMenuBtn.Open();
    }
    
    public void HandleSlotHovered(UIInventorySlot slot)
    {
        UITooltip slotTooltip = UIManager.Instance.Get<UITooltip>();
        
        slotTooltip.SetDescription(slot.itemData.description);
        slotTooltip.SetPosition(slot.transform);
        slotTooltip.Open(true);
    }

    public void HandleSlotUnHovered()
    {
        UITooltip slotTooltip = UIManager.Instance.Get<UITooltip>();
        slotTooltip.Close(true);
    }
}
