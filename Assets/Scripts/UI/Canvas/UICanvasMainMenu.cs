using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 큰 틀이 되는 메인 메뉴로 구성요소는 총 3개
/// </summary>
public class UICanvasMainMenu : UIBase
{
    // 좌측 캐릭터 정보 창
    [FormerlySerializedAs("chatactorInfo")] [SerializeField] private UICharactorInfo charactorInfo;
    
    // 우측 상단 창
    [SerializeField] private UIJellyInfo jellyInfo;
    
    // 우측 버튼 및 창
    [SerializeField] private UICanvasMenuBtn canvasMenuBtn;
    [SerializeField] private UICanvasInventory canvasInventory;
    [SerializeField] private UICanvasStatus canvasStatus;
    
    // 툴팁
    public UITooltip tooltip;
    
    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.Find("Btn_Back").GetComponent<Button>();
        
        charactorInfo = GetComponentInChildren<UICharactorInfo>();
        jellyInfo = GetComponentInChildren<UIJellyInfo>();
        tooltip = GetComponentInChildren<UITooltip>();
        
        canvasMenuBtn =  GetComponentInChildren<UICanvasMenuBtn>();
        canvasInventory = GetComponentInChildren<UICanvasInventory>();
        canvasStatus = GetComponentInChildren<UICanvasStatus>();
    }

    // 필요한 것 초기화 하고 일단 전체 비활성화. 활성화는 Open에서 할 것
    public override void Initialization()
    {
        base.Initialization();
        charactorInfo.Initialization();
        jellyInfo.Initialization();
        canvasMenuBtn.Initialization();
        canvasInventory.Initialization();
        canvasStatus.Initialization();
        
        tooltip.Initialization();
        
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
        UITooltip slotTooltip = UIManager.Instance.canvasMainMenu.tooltip;
        
        slotTooltip.SetDescription(slot.itemData.description);
        slotTooltip.SetPosition(slot.transform);
        slotTooltip.Open(true);
    }

    public void HandleSlotUnHovered()
    {
        UITooltip slotTooltip = UIManager.Instance.canvasMainMenu.tooltip;
        slotTooltip.Close(true);
    }
}
