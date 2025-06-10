using UnityEngine;

public class UIManager : MonoBehaviour
{
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
