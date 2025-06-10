using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ReflectionTest : MonoBehaviour
{

    public float field1;
    [MyTag(2)] public int field2;
    public double field3;
    public string field4;
    
    
    public int Prop1 { get; set; }
    public float Prop2 { get; set; }


    [MyTag(1)]
    public string Method1()
    {
        return null;
    }

    public int Method2()
    {
        return 0;
    }
    private void Awake()
    {
        Type type = this.GetType();
        
        // 필드 변수들을 출력해보기
        // BindingFlags 지정을 해주시는 편
        // BindingFlags는 어떤 기준으로 조건으로 찾을지에 대한 설정임
        // Instance는 static 빼고 일반 변수들
        // | 로 조건 추가 가능 
        // FieldInfo[] fields = ReflectionUtility.GetField<string>(type, info => string.IsNullOrEmpty((string) info.GetValue(this)));
        // FieldInfo[] fields = ReflectionUtility.GetField<string>(type, IsNull);

        // var fields = type.GetFields<double>();
        // foreach (FieldInfo field in fields)
        // {
        //     print($"Name: {field.Name}, Type : {field.FieldType},  Value: {field.GetValue(this)}");
        // }

        // var infos = type.GetProperties<float>();
        // foreach (PropertyInfo field in infos)
        // {
        //     print($"Name: {field.Name}, Type : {field.PropertyType},  Value: {field.GetValue(this)}");
        // }

        // var methods = type.GetMethods<int>();
        // foreach (MethodInfo field in methods)
        // {
        //     print($"Name: {field.Name}, Type : {field.ReturnType}");
        // }

        var infos = type.GetMembers<MethodInfo, MyTagAttribute>()
                        .OrderBy(m => m.GetCustomAttribute<MyTagAttribute>().Order);

        foreach (var info in infos)
        {
            print($"Name: {info.Name}, Type : {info.ReturnType}");
        }
    }

    private bool IsNull(FieldInfo field)
    {
        string value = (string)field.GetValue(this);
        
        return string.IsNullOrEmpty(value);
    }
}
