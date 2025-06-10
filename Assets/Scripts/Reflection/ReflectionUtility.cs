using System;
using System.Linq;
using System.Reflection;

public static class ReflectionUtility
{
    public static FieldInfo[] GetFields<T>(this Type type, Func<FieldInfo, bool> filter = null)
    {
        return type.GetFields(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic)
                   .Where(info => info.FieldType == typeof(T) && (filter?.Invoke(info) ?? true))
                   .ToArray();
    }

    // 프로퍼티 찾아주기
    public static PropertyInfo[] GetProperties<T>(this Type type, Func<PropertyInfo, bool> filter = null)
    {
        return type.GetProperties(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic)
                   .Where(info => info.PropertyType == typeof(T) && (filter?.Invoke(info) ?? true))
                   .ToArray();
    }
    
    // 메소드 찾아주기
    public static MethodInfo[] GetMethods<T>(this Type type, Func<MethodInfo, bool> filter = null)
    {
        // 타입이 따로 없어서 리턴타입으로 찾을 수 있음
        return type.GetMethods(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic)
                   .Where(info => info.ReturnType == typeof(T) && (filter?.Invoke(info) ?? true))
                   .ToArray();
    }
    
    // where 로 제네릭 타입 제한 걸기
    // public static T[] GetMembers<T>(this Type type, Func<T, bool> filter = null) where T : MemberInfo
    // {
    //     return type.GetMembers(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic)
    //                .Where(info => filter?.Invoke(info))
    // }

    
    // 값타입.
    // 일반적으로 값타입의 데이터만 저장하고 있거나 구조체가 저장하는 데이터의 크기가 16바이트 이하인 경우 구조체가 클래스보다 더 효율적
    public struct  MyStruct
    {
        // ref
        // int, float
    }

    // 그 외에는 클래스가 더 효율적
    // 참조타입이니깐
    public class MyClass
    {
        
    }
    
    // where 로 타입 제한
    public static TMember[] GetMembers<TMember, TAttr>(this Type type) where TMember : MemberInfo
    where TAttr : Attribute
    {
        return type.GetMembers(BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic)
                   .Where(info => info.GetCustomAttribute<TAttr>() != null)
                   .OfType<TMember>() // OfType은 받아온 애들을 해당 타입으로 바꿔주는 링큐 구문!
                   .ToArray();
    }
    
}
