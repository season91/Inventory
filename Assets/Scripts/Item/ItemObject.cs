using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;

    private void Reset()
    {
        itemData = Resources.Load<ItemData>($"Item/Data/{name}");
        
        if (itemData == null)
        {
            Debug.LogWarning($"[ItemObject] 리소스 로드 실패: Item/Data/{name} 에 해당하는 ItemData가 없습니다.");
            return;
        }
    }
}
