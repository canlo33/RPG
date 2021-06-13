using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hotkey : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Ability ability;
    public Potion potion;
    public Image icon;
    public Image dragIcon;
    public Image cdIcon;
    public TextMeshProUGUI amountText;
    public float cdTimer;
    private bool isInCd = false;
    private bool isDraging = false;
    public void UpdateHotkeyUI()
    {
        if(ability == null && potion == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            amountText.enabled = false;
        }
        else if(ability != null && potion == null)
        {
            icon.sprite = ability.icon;
            icon.enabled = true;
            amountText.enabled = false;
        }
        else if (ability == null && potion != null)
        {
            icon.sprite = potion.icon;
            icon.enabled = true;
            amountText.enabled = true;
        }
    }
    #region Pointer
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ability == null & potion == null)
            return;
        if (GameMaster.instance.isMousedLocked)
            return;
        TooltipManager.instance.ShowToolTip(ToolTipText());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ability == null & potion == null)
            return;
        TooltipManager.instance.HideToolTip();
    }
    #endregion
    string ToolTipText()
    {
        StringBuilder build = new StringBuilder();
        #region Ability Text
        // If the hotkey hold an ability create a text for ability information.
        if (ability != null)
        {
            build.AppendLine("<size=20><color=red><b>" + ability.abilityName + "</color></b></size>");
            if (ability.isAoe)
                build.AppendLine("[AoE Ability]");
            else if (ability.isOnTaget)
                build.AppendLine("[Targeted Ability]");
            else if (ability.isProjectile)
                build.AppendLine("[Projectile Ability]");
            build.AppendLine();
            build.AppendLine(ability.description);
            build.AppendLine();
            build.Append("<color=#FFDF00>");
            build.AppendLine("Ability Range; " + ability.abilityRange);
            build.AppendLine("Ability Cooldown: " + ability.cdTimer + "sec");
            build.AppendLine("Mana Cost: " + ability.abilityCost);
            build.Append("</color>");
        }
        #endregion
        #region Potion Text
        // If the hotkey hold an ability create a text for ability information.
        else if (potion != null)
        {
            Color color;
            switch (potion.itemRarity)
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
            build.Append("<size=20>").Append(potion.name).AppendLine("</size>");
            build.Append($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>").AppendLine("[" + potion.itemType + " " + potion.itemRarity + "] </color>");
            build.AppendLine();
            build.AppendLine(potion.description);
        }
        #endregion
        return build.ToString();
    }
    private void Update()
    {
        #region Ability / Potion CoolDown
        if (ability == null && potion == null || isDraging)
            return;
        if(ability != null)
        {
            if (ability.isInCd && !isInCd)
            {
                isInCd = true;
                cdIcon.fillAmount = 1;
                cdTimer = ability.cdTimer + 1;
            }
            if (isInCd)
            {
                cdIcon.fillAmount -= 1 / ability.cdTimer * Time.deltaTime;
                cdTimer -= Time.deltaTime;
                if (cdIcon.fillAmount <= 0)
                {
                    isInCd = ability.isInCd;
                }
            }
            if(ability.abilityCost > GameMaster.instance.playerStats.currentMana)
            {
                var color = icon.color;
                color.a = 0.5f;
                icon.color = color;
            }
            else
            {
                var color = icon.color;
                color.a = 1f;
                icon.color = color;
            }
        }
        else if(potion != null)
        {
            amountText.text = "" + potion.amount;
            if (potion.isInCd && !isInCd)
            {
                isInCd = true;
                cdIcon.fillAmount = 1;
                cdTimer = potion.cdTimer + 1;
            }
            if (isInCd)
            {
                cdIcon.fillAmount -= 1 / potion.cdTimer * Time.deltaTime;
                cdTimer -= Time.deltaTime;
                if (cdIcon.fillAmount <= 0)
                {
                    isInCd = potion.isInCd;
                }
            }
            if(potion.amount == 0)
            {
                var color = icon.color;
                color.a = 0.5f;
                icon.color = color;
            }
            else if(potion.amount != 0)
            {
                var color = icon.color;
                color.a = 1f;
                icon.color = color;
            }
        }
        #endregion
    }
    #region Drag&Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        cdIcon.fillAmount = 0;
        isDraging = true;
        isInCd = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        ability = null;
        potion = null;
        if (eventData.pointerDrag.GetComponentInParent<InventorySlot>() != null && eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item.GetType() == typeof(Potion))
        {
            potion =(Potion)eventData.pointerDrag.GetComponentInParent<InventorySlot>().Item;
        }
        else if(eventData.pointerDrag.GetComponentInParent<AbilityPanel>() != null && eventData.pointerDrag.GetComponentInParent<AbilityPanel>().isUnlocked)        
            ability = eventData.pointerDrag.GetComponentInParent<AbilityPanel>().ability;
        else if(eventData.pointerDrag.GetComponent<Hotkey>() != null)
        {
            ability = eventData.pointerDrag.GetComponent<Hotkey>().ability;
            potion = eventData.pointerDrag.GetComponent<Hotkey>().potion;
            eventData.pointerDrag.GetComponent<Hotkey>().ability = null;
            eventData.pointerDrag.GetComponent<Hotkey>().potion = null;
        }
        UpdateHotkeyUI();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (ability == null && potion == null)
            return;
        dragIcon.enabled = true;
        dragIcon.sprite = icon.sprite;
        icon.enabled = false;
        dragIcon.transform.position = Input.mousePosition;
        isInCd = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragIcon.transform.localPosition = Vector3.zero;
        dragIcon.enabled = false;
        cdIcon.fillAmount = 0;
        isDraging = false;
        UpdateHotkeyUI();
    }
    #endregion
}
