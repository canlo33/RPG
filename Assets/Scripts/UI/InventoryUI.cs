using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventoryManager inventory;
    EquipmentManager equipmentManager;
    Animator anim;
    bool isOpen = false;
    public Transform lowerInventory;
    public Transform equipmentInventory;
    public CanvasGroup deletePopup;
    public Texture2D mouseImage;
    public TextMeshProUGUI goldAmount;
    InventorySlot[] invetorySlots;
    public EquipmentSlot[] equipmentSlots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManager.instance;
        equipmentManager = EquipmentManager.instance;
        invetorySlots = lowerInventory.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = equipmentInventory.GetComponentsInChildren<EquipmentSlot>();
        anim = GetComponent<Animator>();
        inventory.onInventoryChangedCallBack += UpdateUI;
        UIManager.instance.onUITriggeredCallBack += CloseButton;
        UpdateUI();
    }
    // Update is called once per frame
    void Update()
    {
        OpenAndClose();
    }
    private void OpenAndClose()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOpen)
                OpenInventory();
            else
                CloseButton();            
        }
    }
    public void OpenInventory()
    {
        UIManager.instance.onUITriggeredCallBack.Invoke();
        GameMaster.instance.MouseUnlock();        
        anim.Play("Open");
        isOpen = true;
        inventory.onInventoryChangedCallBack.Invoke();
    }
    public void CloseButton()
    {
        if (!isOpen)
            return;
        anim.Play("Close");
        GameMaster.instance.MouseLock();
        TooltipManager.instance.HideToolTip();
        deletePopup.alpha = 0;
        deletePopup.blocksRaycasts = false;
        isOpen = false;
    }
    void UpdateUI()
    {
        for (int i = 0; i < invetorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
                invetorySlots[i].DisplayItem(inventory.items[i]);
            else
                invetorySlots[i].ClearSlot();
        }
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentManager.currentEquipment[i] != null)
            {
                equipmentSlots[i].DisplayItem(equipmentManager.currentEquipment[i]);
            }        
            else
                equipmentSlots[i].ClearSlot();
        }
        goldAmount.text = InventoryManager.instance.playerGold.ToString("n0");
    }
}
