using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot
{
    public ItemType itemType;
    public Sprite defaultIcon;
    public Equipment equipment;

    public override void DisplayItem(Item item)
    {
        Item = item;
        icon.sprite = item.icon;
        var tempColor = icon.color;
        tempColor.a = 1f;
        icon.color = tempColor;
        icon.enabled = true;
    }
    public override void ClearSlot()
    {
        Item = null;
        icon.sprite = defaultIcon;
        var tempColor = icon.color;
        tempColor.a = .75f;
        icon.color = tempColor;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (Item == null || InventoryManager.instance.IsInventoryFull())
            return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int slotIndex = (int)Item.itemType;
            EquipmentManager.instance.Unequip(slotIndex);
        }
    }
}
