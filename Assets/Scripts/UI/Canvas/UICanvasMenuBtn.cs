using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasMenuBtn : MonoBehaviour, IGUI
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
        btnStatus = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Btn_Status")?.GetComponent<Button>();
        btnInventory = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Btn_Inventory")?.GetComponent<Button>();

    }

    public void Initialization()
    {
        gameObject.SetActive(false);
        
        btnStatus.onClick.RemoveAllListeners();
        btnInventory.onClick.RemoveAllListeners();

        btnStatus.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Status));
        btnInventory.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(MainButtonType.Inventory));
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        // 버튼 닫아주기
        gameObject.SetActive(false);
    }

}
