using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Player player;
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Reset()
    {
        player = FindObjectOfType<Player>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UIManager.Instance.Init();
        UIManager.Instance.MainStart();
    }

    // 아이템 장착
    public void OnInventorySlotClicked(UIInventorySlot slot)
    {
        if (slot.itemData == null) return;

        // 장착 중이라면
        if (player.equippedItems.Contains(slot.itemData))
        {
            // 장착 해제
            player.UnEquipItem(slot.itemData);
            slot.SetItemEquipUI(false); // 인벤토리 전체 갱신이 아니고 효과도 아니기 떄문에 UIManager 거치지 않고 바로 처리
        }
        else
        {
            // 장착
            player.EquipItem(slot.itemData);
            slot.SetItemEquipUI(true);
        }
    }
}
