using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = System.Object;
[Serializable]
public class SaveDataElement
{
    public string key;
    public string value;
    public string type; // 변환을 위한 변수

    // 생성자 함수
    public SaveDataElement(FieldInfo info, object instance)
    {
        key = info.Name;                            // 필드 변수의 이름을 key값으로 사용.
        value = info.GetValue(instance).ToString(); // 필드 변수의 값을 문자열로 만들어 value로 사용.
        type = ParseType(info.FieldType);           // 타입을 문자열로 저장
    }

    // 타입을 바로 ToString()하면, System.Single, System.Int32같은 값이 나오니,
    // 우리가 알아 보기 쉽도록 타입을 문자열로 만들때, 조금 더 직관적인 이름을 사용하도록 함
    private string ParseType(Type typeToParse)
    {
        if (typeToParse == typeof(int))
            return "INT";
        
        if (typeToParse == typeof(float))
            return "FLOAT";
        
        if (typeToParse == typeof(bool))
            return "BOOL";
        
        if (typeToParse == typeof(string))
            return "STRING";

        return null;
    }

    // type 변수를 참조하여, 문자열 value를 해당 타입의 값으로 반환할 수 있도록 함
    // 사실 enum이 더 안전한 방법이긴 함! 아니면 딕셔너리를 선언해서 그 친구를 통해 파싱하는 것도 방법!
    // 이건 임시 실습용 코드인점 감안
    public object GetValue()
    {
        if (type == "INT")
            return int.Parse(value);
        
        if (type == "FLOAT")
            return float.Parse(value);
        
        if (type == "BOOL")
            return bool.Parse(value);

        if (type == "STRING")
            return value;

        return null;
    }
}

[Serializable]
public class SaveData
{
    public List<SaveDataElement> saveDataElements;
}

public static class SaveLoadManager 
{
    // public static void Save(Object instance)
    // {
    //     // var fields = type.GetMembers<FieldInfo, SaveAttribute>();
    //     
    //     // SaveDataElement ele = new SaveDataElement{key = "이름", value = "조아영"};
    //     //
    //     // string json = JsonUtility.ToJson(ele, true);
    //     //
    //     // Debug.Log(json);
    //     
    //     var fields = instance.GetType().GetMembers<FieldInfo, SaveAttribute>();
    //
    //     var savedata = fields.Select(info => new SaveDataElement()
    //                              { key = info.Name, value = info.GetValue(instance).ToString() })
    //                          .ToList();
    //     
    //     SaveData data = new SaveData(){elements = savedata};
    //     
    //     string json = JsonUtility.ToJson(data, true);
    //     
    //     Debug.Log(json);
    //     
    //     // File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    // }
    
    // 리펙렉션 이용해 저장하는 것
    // instance가 가지고 있는 변수들 중에 savaattr를 가진 애들 걸러내서 배열에 담아 저장
    public static void Save(object instance, string fileName)
    {
        var fields = instance.GetType().GetMembers<FieldInfo, SaveAttribute>();
        
        var saveDataElements = fields
                               .Select(info => new SaveDataElement(info, instance))
                               .ToList();

        SaveData saveData = new SaveData() { saveDataElements = saveDataElements };

        string json = JsonUtility.ToJson(saveData, true);
        
        // Application.dataPath 는 사실 이거 안씀! 쓰면안됨 강의편의상 쓴것
        File.WriteAllText($"{Application.dataPath}/{fileName}", json);
    }
    
    // 로드는 좀 까다로움! 
    public static void Load(object instance, string fileName)
    {
        string fullPath = $"{Application.dataPath}/{fileName}";

        // 해당 경로에 데이터를 저장한(json) 파일이 없을경우 함수 종료
        if (File.Exists(fullPath) == false)
        {
            Debug.Log($"[로드 실패] [{instance}] [{fullPath}]에 해당하는 파일이 없습니다.");
            return;
        }

        // File.ReadAllText()로 파일을 불러들여 문자열로 만들기
        string json = File.ReadAllText(fullPath);

        // JsonUtility.FromJson()을 활용해서, json 데이터를 SaveData 객체로 역직렬화
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        var fields = instance.GetType().GetMembers<FieldInfo, SaveAttribute>();
        
        foreach (var elem in saveData.saveDataElements)
        {
            // SaveDateElement의 key와 동일한 필드를 찾음
            // FirstOrDefault구문은 참고로 넣은 조건에 해당하는 애가 있다면 반환 or 없다면 디폴트값(null) 리턴
            var info = fields.FirstOrDefault(f => f.Name == elem.key);
            
            // 동일한 필드가 있으면, 세이브 파일에 저장되어있던 값을 필드에 적용
            info?.SetValue(instance, elem.GetValue());
        }
    }
}
