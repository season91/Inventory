using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [MainMenu 구성요소] 버튼 눌렀을 때 진입할 메인메뉴 우측 인벤토리 창
/// </summary>
public class UICanvasInventory : UIBase
{
    // 아이템 개수
    public TextMeshProUGUI tmpCount;
    
    // 슬롯 관련
    public Transform scrollViewContent;
    public GameObject slotPrefab;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransf = GetComponent<RectTransform>();
        btnBack = transform.parent.Find("Btn_Back").GetComponent<Button>();
        
        tmpCount = transform.Find("Group_Inventory/Tmp_Count").GetComponent<TextMeshProUGUI>();
        scrollViewContent = transform.Find("Group_Inventory/Scroll View/Viewport/Content").GetComponent<Transform>();
        slotPrefab = Resources.Load<GameObject>("GUI/Group_Slot");
    }

    public override void Open(bool showBackButton)
    {
        base.Open(showBackButton);
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        List<ItemData> inventoryItems = GameManager.Instance.player.inventoryItems;
        List<ItemData> equippedItems = GameManager.Instance.player.equippedItems;
        
        // 기존것 파괴
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // scrollViewContent 사용하려면은 
        foreach (ItemData item in inventoryItems)
        {
            GameObject slotGO = Instantiate(slotPrefab, scrollViewContent);
            UIInventorySlot slotUI = slotGO.GetComponent<UIInventorySlot>();
            slotUI.SetItemSlot(item); // 슬롯에 데이터 설정
            
            // 장착 아이템 확인
            slotUI.SetItemEquipUI(equippedItems.Contains(item));
            
            // 클릭시 이벤트 함수는 GameManager에 로직 처리되어있어서 등록처리만
            slotUI.OnSlotClicked += GameManager.Instance.OnInventorySlotClicked;
        }
        
        tmpCount.text = inventoryItems.Count.ToString();
    }
}
