using UnityEngine;
/// <summary>
/// [MainMenu 구성요소] 좌측 캐릭터 정보창
/// </summary>
public class UICanvasStatus : MonoBehaviour, IGUI
{
    private IGUI iguiImplementation;
    
    public void Initialization()
    {
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
