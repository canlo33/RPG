using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public TextMeshProUGUI title;
    public List<Item> items;
    public GameObject itemPanel;
    ItemSlot[] storeSellSlots;
    public GameObject cart;
    ItemSlot[] cartSlots;
    public List<Item> cartItems;
    public GameObject playerInventory;
    public ItemSlot[] playerInventorySlots;
    public List<Item> inventoryItems;
    public int totalAmount;
    public TextMeshProUGUI totalAmountText;
    public TextMeshProUGUI playerGoldText;
    public Image buyButtonImage;
    public Image sellButtonImage;
    public TextMeshProUGUI buttonText;
    private InventoryManager inventoryManager;
    private CanvasGroup canvasGroup;
    public bool isOpen = false;
    public bool isSelling = true;
    public bool isCartFull = false;
    private Color switchColor;
    public StoreNpc npc;
    [SerializeField] private AudioClip buysellAudioClip;
    [SerializeField] private AudioClip buttonClickAudio;
    public AudioClip addToCartAudio;
    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        UIManager.instance.onUITriggeredCallBack += CloseStore;
        storeSellSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
        playerInventorySlots = playerInventory.GetComponentsInChildren<ItemSlot>();
        cartSlots = cart.GetComponentsInChildren<ItemSlot>();
        canvasGroup = GetComponent<CanvasGroup>();
        switchColor = new Color32(32, 142, 142, 255);
        UpdateUI();
        SwitchToBuy();
    }
    public void UpdateUI()
    {
        ClearAllSlots();
        totalAmount = 0;
        playerGoldText.text = InventoryManager.instance.playerGold.ToString("n0");
        inventoryItems = inventoryManager.items;
        UpdateAllSlots();        
        totalAmountText.text = totalAmount.ToString("n0");
        if (isSelling)
            buttonText.text = "Sell";
        else
            buttonText.text = "Buy";
    }
    public void SetTitle(string text)
    {
        title.text = text;
    }
    public bool IsCartFull()
    {
        return cartItems.Count == cartSlots.Length;
    }
    private void ClearAllSlots()
    {
        foreach (var storeSlot in storeSellSlots)
            storeSlot.ClearSlot();
        foreach (var cartSlot in cartSlots)
            cartSlot.ClearSlot();   
        foreach (var playerInventorySlot in playerInventorySlots)
            playerInventorySlot.ClearSlot();        
    }
    private void UpdateAllSlots()
    {
        for (int i = 0; i < items.Count; i++)
            storeSellSlots[i].item = items[i];
        for (int i = 0; i < cartItems.Count; i++)
        {
            cartSlots[i].item = cartItems[i];
            if(isSelling)
                totalAmount += cartItems[i].sellPrice;
            else
                totalAmount += cartItems[i].sellPrice * 2;
        }
        for (int i = 0; i < inventoryItems.Count; i++)
            playerInventorySlots[i].item = inventoryItems[i];
        foreach (var storeSlot in storeSellSlots)
            storeSlot.UpdateUI();
        foreach (var cartSlot in cartSlots)
            cartSlot.UpdateUI();
        foreach (var playerInventorySlot in playerInventorySlots)
            playerInventorySlot.UpdateUI();
    }
    public void SwitchToBuy()
    {
        isSelling = false;
        buyButtonImage.color = switchColor;
        sellButtonImage.color = Color.black;
        cartItems.Clear();
        foreach (var playerInventorySlot in playerInventorySlots)
            playerInventorySlot.isItemInCart = false;
        UpdateUI();
    }
    public void SwitchToSell()
    {
        isSelling = true;
        buyButtonImage.color = Color.black;
        sellButtonImage.color = switchColor;
        cartItems.Clear();
        foreach (var playerInventorySlot in playerInventorySlots)
            playerInventorySlot.isItemInCart = false;
        UpdateUI();
    }
    public void InteractButton()
    {
        if (isSelling && cartItems.Count != 0)
        {
            inventoryManager.AddGold(totalAmount);
            cartItems.ForEach((t) => { inventoryManager.RemoveItem(t); });
            npc.PlaySoundEffect(buysellAudioClip);
        }
        else if(!isSelling && totalAmount <= inventoryManager.playerGold)
        {
            if (inventoryItems.Count + cartItems.Count > inventoryManager.inventorySize)
                return;
            cartItems.ForEach((t) => { inventoryManager.AddItem(t, t.amount); });
            inventoryManager.AddGold(-totalAmount);
            npc.PlaySoundEffect(buysellAudioClip);
        }
        cartItems.Clear();
        foreach (var playerInventorySlot in playerInventorySlots)
            playerInventorySlot.isItemInCart = false;
        UpdateUI();
    }
    public void OpenStore()
    {
        UIManager.instance.onUITriggeredCallBack.Invoke();
        cartItems.Clear();
        SwitchToBuy();
        UpdateUI();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isOpen = true;
        GameMaster.instance.MouseUnlock();
    }
    public void CloseStore()
    {
        cartItems.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isOpen = false;
        GameMaster.instance.MouseLock();
        npc.PlaySoundEffect(buttonClickAudio);
    }
}
