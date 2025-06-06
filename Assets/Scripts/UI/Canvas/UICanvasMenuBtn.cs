using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasMenuBtn : MonoBehaviour, IGUI
{
    // 우측 버튼
    [SerializeField] private Button btnStatus;
    [SerializeField] private Button btnInventory;
    [SerializeField] private Button btnBack;

    // 버튼 처리 델리게이트 선언
    public delegate void MenuButtonClickHandler(MainButtonType type);
    
    // OnMenuButtonClicked 이벤트는 UICanvasMainMenu에서 넣어줄 것
    public event MenuButtonClickHandler OnMenuButtonClicked;
    
    private void Reset()
    {
        btnStatus = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Btn_Status")?.GetComponent<Button>();
        btnInventory = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Btn_Inventory")?.GetComponent<Button>();
        btnBack = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Btn_Back")?.GetComponent<Button>();
    }

    public void Initialization()
    {
        gameObject.SetActive(false);
        
        btnStatus.onClick.RemoveAllListeners();
        btnInventory.onClick.RemoveAllListeners();
        btnBack.onClick.RemoveAllListeners();

        btnStatus.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Status));
        btnInventory.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Inventory));
        btnBack.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Back));
    }

    public void Open()
    {
        gameObject.SetActive(true);
        CloseBackBtn();
    }

    public void Close()
    {
        CloseMainBtn();
    }
    
    public void OpenMainBtn()
    {
        btnStatus.gameObject.SetActive(true);
        btnInventory.gameObject.SetActive(true);
    }

    public void OpenBackBtn()
    {
        btnBack.gameObject.SetActive(true);
    }
    
    public void CloseBackBtn()
    {
        btnBack.gameObject.SetActive(false);
    }

    private void CloseMainBtn()
    {
        btnStatus.gameObject.SetActive(false);
        btnInventory.gameObject.SetActive(false);
    }
}
