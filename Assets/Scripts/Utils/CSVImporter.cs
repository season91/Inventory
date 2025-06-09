using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CSVImporter
{
    [MenuItem("Tools/Import/Items")]
    public static void CSVImport()
    {
        string path = EditorUtility.OpenFilePanel($"Select CSV", "", "csv");
        if (string.IsNullOrEmpty(path)) return;
        
        string[] lines = File.ReadAllLines(path);
        if (lines.Length <= 1) {
            Debug.LogWarning("CSV has no data.");
            return;
        }
        
        string targetFolder = $"Assets/Resources/Item/Data/";
        if (!Directory.Exists(targetFolder)) {
            Directory.CreateDirectory(targetFolder);
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cols = lines[i].Split(',');
            
            var item = ScriptableObject.CreateInstance<ItemData>();
            item.itemName = cols[0];
            item.icon = IconParse(cols[1]);
            item.statType = Enum.Parse<StatType>(cols[2]);
            item.statValue = float.Parse(cols[3]);
            item.description = cols[4];
            // item.itemType = Enum.Parse<ItemType>(cols[2]);
            // if (cols[3] == null || cols[3] == "")
            // {
            //     ItemDataConsumable cdata = new ItemDataConsumable();
            //     cdata.types = ParseEnums<ConsumableType>(cols[4]);
            //     cdata.amounts = ParseFloats(cols[5]);
            //     item.consumableData = cdata;
            // }
            // else
            // {
            //     ItemDataEquipable edata = new  ItemDataEquipable();
            //     edata.equipType = Enum.Parse<EquipableType>(cols[3].Trim());
            //     edata.statType = Enum.Parse<StatType>(cols[4]);
            //     edata.amount = int.Parse(cols[5]);
            //     item.equipableData = edata;
            // }
            
            string assetPath = $"{targetFolder}/{item.itemName}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);
            if (existing != null)
            {
                EditorUtility.CopySerialized(item, existing);
                EditorUtility.SetDirty(existing);
            }
            else
            {
                AssetDatabase.CreateAsset(item, assetPath);
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"CSV import completed.");
    }
    
    public static Sprite IconParse(string iconName)
    {
        string path = $"Item/Icons/{iconName.Trim()}";
        Sprite icon = Resources.Load<Sprite>(path);
        if (icon == null)
            Debug.LogWarning($"[IconParse] Icon not found: {path}");
        return icon;
    }
    
    private static TEnum[] ParseEnums<TEnum>(string value) where TEnum : struct
    {
        string[] tokens = value.Split('|');
        // 공백제거한 후 조회
        return tokens.Select(t => t.Trim())
                     .Where(t=> Enum.TryParse<TEnum>(t, out _))
                     .Select(t => Enum.Parse<TEnum>(t))
                     .ToArray();
    }
    
    private static float[] ParseFloats(string value)
    {
        return value.Split('|').Select(float.Parse).ToArray();
    }

}

