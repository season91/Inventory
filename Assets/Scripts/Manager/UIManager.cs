using UnityEngine;

public class UIManager : MonoBehaviour
{
    // STEP 3. 싱글톤으로 만들기
    private static UIManager instance;
    
    public static UIManager Instance 
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // 고정 UI 3개 
    // STEP 3. 프로퍼티로 만들기
    [SerializeField] private UICanvasMainMenu CanvasMainMenu;
    
    
    private void Reset()
    {
        CanvasMainMenu = GetComponentInChildren<UICanvasMainMenu>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Init()
    {
        CanvasMainMenu.Initialization();
    }

    public void MainStart()
    {
        CanvasMainMenu.Open();
    }
}
