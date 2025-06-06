using UnityEngine;

/// <summary>
/// 큰 틀이 되는 메인 메뉴로 구성요소는 총 3개
/// </summary>
public class UICanvasMainMenu : MonoBehaviour, IGUI
{
    private IGUI iguiImplementation;
    
    // 좌측 캐릭터 정보 창
    [SerializeField] private UIChatactorInfo chatactorInfo;
    
    // 우측 버튼 및 창
    [SerializeField] private UICanvasMenuBtn canvasMenuBtn;
    [SerializeField] private UICanvasInventory canvasInventory;
    [SerializeField] private UICanvasStatus canvasStatus;
    
    private void Reset()
    {
        chatactorInfo = GetComponentInChildren<UIChatactorInfo>();
        
        canvasMenuBtn =  GetComponentInChildren<UICanvasMenuBtn>();
        canvasInventory = GetComponentInChildren<UICanvasInventory>();
        canvasStatus = GetComponentInChildren<UICanvasStatus>();
    }

    // 필요한 것 초기화 하고 일단 전체 비활성화. 활성화는 Open에서 할 것
    public void Initialization()
    {
        chatactorInfo.Initialization(); 
        
        canvasMenuBtn.Initialization();
        canvasInventory.Initialization(); 
        canvasStatus.Initialization();

        canvasMenuBtn.OnMenuButtonClicked += HandleMenuButtonClicked;
    }

    // 메인메뉴 진입시 좌측 설명 Open
    public void Open()
    {
        chatactorInfo.Open();
        canvasMenuBtn.Open();
    }

    public void Close()
    {
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
        canvasStatus.Open();
        canvasMenuBtn.Close();
        canvasMenuBtn.OpenBackBtn();
    }

    private void ShowInventory()
    {
        canvasInventory.Open();
        canvasMenuBtn.Close();
        canvasMenuBtn.OpenBackBtn();
    }

    private void ShowMainMenu()
    {
        Debug.Log("ShowMainMenu");
        if(canvasStatus.gameObject.activeSelf) canvasStatus.Close();
        if(canvasInventory.gameObject.activeSelf) canvasInventory.Close();
        canvasMenuBtn.OpenMainBtn();
    }
}
