using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string userName;
    public DateTime birthDate;   // 생년월일
    public float weight;         // 몸무게 (kg)
    public int mood;             // 기분
    public int stamina;          // 활동력 (0~100)
    public int fullness;         // 포만감 (0~100)
    public int social;           // 사회성 (0~100)
    public string introduction;  // 자기소개
    public int jelly;            // 화폐
    
    public int GetAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age;
    }

    public void AddStat(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Weight:
                weight += amount;
                break;
            case StatType.Mood:
                mood += Mathf.RoundToInt(amount);
                break;
            case StatType.Stamina:
                stamina += Mathf.Clamp(stamina + Mathf.RoundToInt(amount), 0, 100);
                break;
            case StatType.Fullness:
                fullness += Mathf.Clamp(fullness + Mathf.RoundToInt(amount), 0, 100);
                break;
            case StatType.Social:
                social += Mathf.Clamp(social + Mathf.RoundToInt(amount), 0, 100);
                break;
        }

    }
    
    public void RemoveStat(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Weight:
                weight -= amount;
                break;
            case StatType.Mood:
                mood -= Mathf.RoundToInt(amount);
                break;
            case StatType.Stamina:
                stamina -= Mathf.Clamp(stamina + Mathf.RoundToInt(amount), 0, 100);
                break;
            case StatType.Fullness:
                fullness -= Mathf.Clamp(fullness + Mathf.RoundToInt(amount), 0, 100);
                break;
            case StatType.Social:
                social -= Mathf.Clamp(social + Mathf.RoundToInt(amount), 0, 100);
                break;
        }
    }
}

public class Player : MonoBehaviour
{
    [MyTag(3)] public PlayerData data;

    public List<ItemData> inventoryItems; // 가지고 있는 아이템
    public List<ItemData> equippedItems; // 장착한 아이템
    
    [SerializeField] private Transform spriteTransform; 
    private void Awake()
    {
        data = new PlayerData
        {
            userName = "조프림",
            birthDate = new DateTime(2021, 4, 29),
            mood = 20,
            weight = 4.3f,
            stamina = 80,
            fullness = 50,
            social = 15,
            introduction = "조씨집안 서열 1위\nMBTI : CUTE\n인스타 : @Premeee___\n팔로워 1,000명 보유\n유튜브 데뷔 준비 중\n좋아하는 거 고구마\n싫어하는 거 산책",
            jelly = 20000
        };

        spriteTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        StartIdleTilt();
    }

    public void EquipItem(ItemData item)
    {
        equippedItems.Add(item);
        data.AddStat(item.statType, item.statValue);
    }
    public void UnEquipItem(ItemData item)
    {
        equippedItems.Remove(item);
        data.RemoveStat(item.statType, item.statValue);
    }
    
    private void StartIdleTilt()
    {
        spriteTransform
            .DOLocalRotate(new Vector3(0, 0, 5f), 0.5f)
            .SetLoops(-1, LoopType.Yoyo) // 무한 반복: 왔다갔다
            .SetEase(Ease.InOutSine);
    }

}
