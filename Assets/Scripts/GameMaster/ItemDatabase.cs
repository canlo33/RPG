using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemDatabase instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one ItemDatabase instance is found!");
            return;
        }
        instance = this;
    }
    #endregion
    public List<Item> itemList = new List<Item>();
    public Item FindItem(string itemName)
    {
        foreach (var item in itemList)
        {
            if (item.name == itemName)
                return item;
        }
        return null;
    }
}
