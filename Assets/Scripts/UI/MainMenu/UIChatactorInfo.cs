using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChatactorInfo : MonoBehaviour, IGUI
{
    private IGUI iguiImplementation;
    // 좌측 구성요소
    [SerializeField] private TextMeshProUGUI tmpName;
    [SerializeField] private TextMeshProUGUI tmpDescription;
    [SerializeField] private TextMeshProUGUI tmpAge;
    [SerializeField] private Image imgExpBar;
    
    private void Reset()
    {
        tmpName = GetComponentsInChildren<Transform>(true)
                  .FirstOrDefault(t => t.name == "Tmp_Name")?.GetComponent<TextMeshProUGUI>();
        tmpDescription = GetComponentsInChildren<Transform>(true)
                         .FirstOrDefault(t => t.name == "Tmp_Description")?.GetComponent<TextMeshProUGUI>();
        tmpAge = GetComponentsInChildren<Transform>(true)
                 .FirstOrDefault(t => t.name == "Tmp_Age")?.GetComponent<TextMeshProUGUI>();
        imgExpBar = GetComponentsInChildren<Transform>(true)
                    .FirstOrDefault(t => t.name == "Img_ExpBar")?.GetComponent<Image>();
    }

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
