using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameMaster gameMaster;
    private StartManager startManager;
    public List<Item> items = new List<Item>();
    public List<Item> starterItems = new List<Item>();
    public int inventorySize;
    public int playerGold;
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallBack;
    public AudioClip itemEquippedSound;
    #region Singleton
    public static InventoryManager instance;   

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one inventory instance is found!");
            return;
        }         
        instance = this;
    }
    #endregion
    public void AddItem(Item item, int amount)
    {
        if(IsInventoryFull() && !item.stackable)
        {
            Debug.Log("Inventory is full");
            return;
        }
        if (item.stackable && items.Contains(item))
        {
            //Add more amount on top of the existing item.
            items.Find(i => i == item).amount += amount;
        }
        else
        {
            item.amount = amount;
            items.Add(item);
        }
        if (onInventoryChangedCallBack != null)
            onInventoryChangedCallBack.Invoke();
    }
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
            items.Remove(item);
        else return;
        if (onInventoryChangedCallBack != null)
            onInventoryChangedCallBack.Invoke();
    }
    public bool IsInventoryFull()
    {
        return inventorySize <= items.Count;
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        if (onInventoryChangedCallBack != null)
            onInventoryChangedCallBack.Invoke();
    }

    private void SaveInventory()
    {
        startManager.savedData.playerGold = playerGold;
        foreach (var item in items)
        {
            startManager.savedData.items.Add(item.name);
            startManager.savedData.itemAmount.Add(item.amount);
        }        
    }
    private void LoadInventory()
    {
        playerGold = startManager.savedData.playerGold;
        items.Clear();
        for (int i = 0; i < startManager.savedData.items.Count; i++)
        {
            Item loadedItem = ItemDatabase.instance.FindItem(startManager.savedData.items[i]);
            int amount = startManager.savedData.itemAmount[i];
            AddItem(loadedItem, amount);
        }        
    }

    private void Start()
    {
        gameMaster = GameMaster.instance;
        startManager = StartManager.instance;
        startManager.OnSaveSystemCallBack += SaveInventory;
        startManager.OnLoadSystemCallBack += LoadInventory;
        foreach (Item item in starterItems)
        {
            AddItem(item, 1);
        }
    }
}
