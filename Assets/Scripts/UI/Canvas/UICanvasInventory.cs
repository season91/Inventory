using UnityEngine;
/// <summary>
/// [MainMenu 구성요소] 버튼 눌렀을 때 진입할 메인메뉴 우측 인벤토리 창
/// </summary>
public class UICanvasInventory : MonoBehaviour, IGUI
{
    private IGUI iguiImplementation;

    public void Initialization()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
    }
}
