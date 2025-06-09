using System;
using UnityEngine;

// 스탯 
public enum StatType
{
    Mood,
    Stamina,
    Social,
    Fullness,
    Weight
}
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public StatType statType;
    public float statValue;
    public string description;
}
