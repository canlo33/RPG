using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public TextMeshProUGUI amountText;
    private CanvasGroup canvasGroup;        
    public Item Item { get; set; }
    private void Start()
    {
        canvasGroup = icon.GetComponent<CanvasGroup>();        
    }
    public virtual void DisplayItem(Item item)
    {
        Item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        if (item.amount > 1)
            amountText.text = "" + item.amount;
        else
            amountText.text = null;
    }
    public virtual void ClearSlot()
    {
        Item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = null;
    }
    #region Pointer
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (Item == null)
            return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Item.Use();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item == null)
            return;
        TooltipManager.instance.ShowToolTip(ToolTipText());

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Item == null)
            return;
        TooltipManager.instance.HideToolTip();
    }
    #endregion
    public virtual string ToolTipText()
    {
        StringBuilder build = new StringBuilder();
        Color color;
        switch (Item.itemRarity)
        {
            case Rarity.Common:
                color = Color.white;
                break;
            case Rarity.Uncommon:
                color = Color.green;                
                break;
            case Rarity.Rare:
                color = Color.blue;
                break;
            case Rarity.Epic:
                color = new Color(143, 0, 254, 1);
                break;
            default:
                color = Color.white;
                break;
        }
        build.Append("<size=20>").Append(Item.name).AppendLine("</size>");
        build.Append($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>").AppendLine("[" + Item.itemType + " " +Item.itemRarity + "] </color>");
        build.AppendLine();
        if(Item.GetType() == typeof(Item))
        {
            build.AppendLine(Item.description);
        }
        else if (Item.GetType() == typeof(Equipment))
        {
            build.Append("<color=#FFDF00>");
            if (Item.attack != 0)
                build.AppendLine("Attack Bonus: " + Item.attack);
            if (Item.defense != 0)
                build.AppendLine("Defense Bonus: " + Item.defense);
            if (Item.health != 0)
                build.AppendLine("Health Bonus: " + Item.health);
            if (Item.mana != 0)
                build.AppendLine("Mana Bonus: " + Item.mana);
            build.AppendLine("</color>");
            build.Append("Right Click To Equip!");
        }
        build.AppendLine();
        build.AppendLine("Sell Price: " + "<color=red><b>"+Item.sellPrice+ " Gold" + "</color></b>");
        return build.ToString();
    }

    #region Drag&Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null || GetType() == typeof(EquipmentSlot))
            return;
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null || GetType() == typeof(EquipmentSlot))
            return;
        icon.transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item == null || GetType() == typeof(EquipmentSlot))
            return;
        icon.transform.localPosition = Vector3.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion
}
