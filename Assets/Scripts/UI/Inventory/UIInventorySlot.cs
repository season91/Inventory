using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image imgItemIcon;
    public ItemData itemData;
    [SerializeField] private Button itemBtn;
    [SerializeField] private GameObject tmpItemEquip;
    
    // UICanvasInventory 에서 해당 Slot 생성할때 등록해줄 것
    public Action<UIInventorySlot> OnSlotClicked;
    
    // tooltip 델리게이트 
    public Action<UIInventorySlot> OnSlotHovered;
    public Action OnSlotUnhovered;
    
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

    // 아이템 툴팁을 위해 사용
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSlotHovered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnSlotUnhovered?.Invoke();
    }
}
