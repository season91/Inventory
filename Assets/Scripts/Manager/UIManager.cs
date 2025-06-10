using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private Dictionary<Type, UIBase> uiBaseDict = new();
    public static UIManager Instance 
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    public UICanvasMainMenu canvasMainMenu;
    
    private void Reset()
    {
        canvasMainMenu = GetComponentInChildren<UICanvasMainMenu>();
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
        var uiBases = transform.GetComponentsInChildren<UIBase>();
        for (int i = 0; i < uiBases.Length; i++)
        {
            uiBases[i].Initialization();
            uiBaseDict.Add(uiBases[i].GetType(), uiBases[i]);
        }
    }

    public void MainStart()
    {
        canvasMainMenu.Open();
    }

    public T Get<T>() where T : UIBase
    {
        var type = typeof(T);

        if (uiBaseDict.ContainsKey(type))
        {
            return uiBaseDict[type] as T;
        }
        else
        {
            return null;
        }
    }
}
