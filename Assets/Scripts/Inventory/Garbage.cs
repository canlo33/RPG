using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Garbage : MonoBehaviour, IDropHandler
{
    public CanvasGroup deletePopup;
    private Item item;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item == null || eventData.pointerDrag.GetComponentInParent<EquipmentSlot>() != null)
            return;
        //Display Are you Sure PopUp.
        deletePopup.alpha = 1;
        deletePopup.blocksRaycasts = true;
        deletePopup.interactable = true;
        item = eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item;
    }

    public void ConfirmButton()
    {
        InventoryManager.instance.RemoveItem(item);
        CancelButton();
    }
    public void CancelButton()
    {
        deletePopup.alpha = 0;
        deletePopup.blocksRaycasts = false;
        deletePopup.interactable = false;
    }
}
