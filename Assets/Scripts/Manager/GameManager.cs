using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        
    }

    private void Start()
    {
        UIManager.Instance.Init();
        UIManager.Instance.MainStart();   
    }
    
}
