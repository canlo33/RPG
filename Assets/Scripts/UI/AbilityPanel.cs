using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class AbilityPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Ability ability;
    public Image icon;
    private Image copyIcon;
    public Sprite lockedIconImage;
    private TextMeshProUGUI textMesh;
    public bool isUnlocked = false;
    private void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = ability.abilityName;
        icon.sprite = lockedIconImage;
        copyIcon = Instantiate(icon, icon.transform.parent);
        copyIcon.sprite = ability.icon;
        copyIcon.color = Color.white;
        copyIcon.raycastTarget = false;
        copyIcon.enabled = false;
    }
    public void UpdatePanelUI()
    {
        if (GameMaster.instance.playerStats.level >= ability.abilityLevel)
        {
            isUnlocked = true;
            icon.sprite = ability.icon;
            icon.color = Color.white;
        }
    }
    #region Pointer
    public void OnPointerEnter(PointerEventData eventData)
    {
        StringBuilder build = new StringBuilder();
        build.AppendLine("<size=20><b><color=red>" + ability.abilityName + "</size></b></color>");
        build.AppendLine();
        build.AppendLine(ability.description);
        build.AppendLine();
        build.Append("<color=#FFDF00>");
        build.AppendLine("Mana Cost: " + ability.abilityCost);
        build.AppendLine("CoolDown: " + ability.cdTimer +" sec");
        build.AppendLine("Cast Time: " + ability.animationStartTimer + " sec");
        build.Append("<color=#FFDF00>");        
        TooltipManager.instance.ShowToolTip(build.ToString());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideToolTip();
    }


    #endregion Pointer

    #region Drag&Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isUnlocked)
            return;
        copyIcon.enabled = true;
        TooltipManager.instance.HideToolTip();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isUnlocked)
            return;
        copyIcon.transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isUnlocked)
            return;
        copyIcon.enabled = false;
    }
    #endregion
}
