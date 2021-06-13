using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TextMeshProUGUI amountText;
    public Item item;
    public bool isCart;
    public bool isStore;
    public bool isInventory;
    public bool isItemInCart = false;
    private Store store;
    private InventoryManager inventoryManager;

    private void Start()
    {
        store = GetComponentInParent<Store>();
        inventoryManager = InventoryManager.instance;
        isItemInCart = false;
    }
    public virtual void DisplayItem()
    {
        icon.sprite = item.icon;
        icon.enabled = true;
        if (item.amount > 1)
            amountText.text = "" + item.amount;
        else
            amountText.text = null;
    }
    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = null;
    }
    public void UpdateUI()
    {
        if (item != null)
            DisplayItem();
        else
            ClearSlot();
    }
    public void AddToCart(Item item)
    {
        if(!store.isSelling)
            store.cartItems.Add(item);
        else if(store.isSelling)
        {
            if(!isItemInCart)
            {
                store.cartItems.Add(item);
                isItemInCart = true;
            }                
        }
        store.UpdateUI();
    }
    public void RemoveFromCart()
    {
        if (store.isSelling)
        {
            store.totalAmount -= item.sellPrice;
            int index = store.inventoryItems.IndexOf(item);
            store.playerInventorySlots[index].isItemInCart = false;
        }            
        else if (!store.isSelling)
            store.totalAmount -= item.sellPrice * 2;
        store.cartItems.Remove(item);
        ClearSlot();
        store.UpdateUI();
    }
    #region Sell&Buy
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        store.isCartFull = store.IsCartFull();
        if (item == null || store.isCartFull)
            return;
        if (store.isSelling)
        {
            if (eventData.button == PointerEventData.InputButton.Right && isInventory)
            {
                AddToCart(item);
                store.npc.PlaySoundEffect(store.addToCartAudio);
            }               
            else if (eventData.button == PointerEventData.InputButton.Right && isCart)
            {
                RemoveFromCart();
                store.npc.PlaySoundEffect(store.addToCartAudio);
            }                
        }
        else if (!store.isSelling)
        {
            if (eventData.button == PointerEventData.InputButton.Right && isStore)
            {
                AddToCart(item);
                store.npc.PlaySoundEffect(store.addToCartAudio);
            }
                
            else if (eventData.button == PointerEventData.InputButton.Right && isCart)
            {
                RemoveFromCart();
                store.npc.PlaySoundEffect(store.addToCartAudio);
            }                
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;
        //TooltipManager.instance.ShowToolTip(ToolTipText());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;
        //TooltipManager.instance.HideToolTip();
    }
    #endregion
}
