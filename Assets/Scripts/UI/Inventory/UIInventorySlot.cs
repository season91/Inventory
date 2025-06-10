using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image imgItemIcon;
    public ItemData itemData;
    [SerializeField] private Button itemBtn;
    [SerializeField] private GameObject tmpItemEquip;
    
    // UICanvasInventory 에서 해당 Slot 생성할때 등록해줄 것
    public Action<UIInventorySlot> OnSlotClicked;
    
    private void Reset()
    {
        imgItemIcon = GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "Img_Item_Icon")?.GetComponent<Image>();
        itemBtn = GetComponent<Button>();
    }
    private void Start()
    {
        itemBtn.onClick.AddListener(() => OnSlotClicked?.Invoke(this));
    }

    public void SetItemSlot(ItemData item)
    {
        imgItemIcon.enabled = true;
        imgItemIcon.sprite = item.icon;
        itemData = item;
        SetItemEquipUI(false);
    }

    public void SetItemEquipUI(bool isEquipped)
    {
        tmpItemEquip.SetActive(isEquipped);
    }

}
