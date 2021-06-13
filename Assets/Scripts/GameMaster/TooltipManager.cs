using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipManager : MonoBehaviour
{
    #region Singleton
    public static TooltipManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Tooltip Manager instance found");
            return;
        }
        instance = this;
    }
    #endregion
    public CanvasGroup tooltip;
    public TextMeshProUGUI textMesh;
    public RectTransform tooltipPopup;
    private bool isActive = false;
    private Vector3 offset;
    private ToolTipTimer toolTipTimer;
    public void ShowToolTip(string text, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        textMesh.text = text;
        tooltip.alpha = 1;
        isActive = true;
    }
    public void HideToolTip()
    {
        tooltip.alpha = 0;
        isActive = false;
    }
    public void PositionTooltip()
    {
        if (!isActive || GameMaster.instance.isMousedLocked)
            return;
        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        float pivotX = newPos.x / Screen.width;
        float pivotY = newPos.y / Screen.height;
        if (pivotX >= 0.5)
            offset = new Vector2(-85, 0);
        else
            offset = new Vector2(35, 0);
        newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        pivotX = newPos.x / Screen.width;
        pivotY = newPos.y / Screen.height;
        tooltipPopup.pivot = new Vector2(pivotX, pivotY);
        tooltipPopup.transform.position = newPos;
    }
    private void Update()
    {
        PositionTooltip();
        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
                HideToolTip();
        }
    }
    public class ToolTipTimer
    {
        public float timer;
    }

}
