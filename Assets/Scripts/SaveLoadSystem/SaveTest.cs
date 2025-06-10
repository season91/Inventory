using System;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    [Save, SerializeField] private int level;
    [Save, SerializeField] private int hp;
    [Save, SerializeField] private float stamina;
    [Save, SerializeField] private string nickName;

    //[Save] // 필드변수들이 아닌 메소드도 저장 가능하지만 의미없으니까 제한 걸ㅈ기
    public void Method()
    {
        
        
    }


    private void OnEnable()
    {
        SaveLoadManager.Load(this, "save.json");
    }

    private void OnDisable()
    {
        SaveLoadManager.Save(this, "save.json");
    }

    private void Awake()
    {
        // SaveLoadManager.Save(this, "save");
    }
}
