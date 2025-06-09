using System;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;

    private void Reset()
    {
        itemData = Resources.Load<ItemData>($"Item/Data/{name}");
    }
}
