using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one EquipmentManager Instance found");
            Destroy(this);
            return;
        }
        instance = this;
        StartManager.instance.OnSaveSystemCallBack += SaveEquipment;
        StartManager.instance.OnLoadSystemCallBack += LoadEquipment;
    }
    #endregion
    public Equipment[] currentEquipment;
    private InventoryManager inventoryManager;
    private void Start()
    {
        inventoryManager = InventoryManager.instance;
    }
    public void Equip(Equipment equipment)
    {
        int slotIndex = (int)equipment.itemType;
        Equipment oldEquipment = null;
        // Check if there is already an item in the equipment slot that we are typing to equip an item on.
        if(currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            oldEquipment.RemoveItemStats();
            inventoryManager.AddItem(oldEquipment, oldEquipment.amount);
        }
        currentEquipment[slotIndex] = equipment;
        equipment.AddItemStats();
        inventoryManager.onInventoryChangedCallBack.Invoke();
    }
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] == null || inventoryManager.IsInventoryFull())
            return;
        Equipment oldEquipment = currentEquipment[slotIndex];
        inventoryManager.AddItem(oldEquipment, oldEquipment.amount);
        currentEquipment[slotIndex] = null;
        oldEquipment.RemoveItemStats();
        inventoryManager.onInventoryChangedCallBack.Invoke();
    }
    private void SaveEquipment()
    {
        if (currentEquipment == null)
            return;
        foreach (var item in currentEquipment)
        {
            if (item == null)
                continue;
            StartManager.instance.savedData.equipments.Add(item.name);
        }
    }
    private void LoadEquipment()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            currentEquipment[i] = null;
        }
        foreach (var item in StartManager.instance.savedData.equipments)
        {
            Equipment equipment = (Equipment)ItemDatabase.instance.FindItem(item);
            inventoryManager.AddItem(equipment, 1);
            equipment.Use();
        }
    }
}
