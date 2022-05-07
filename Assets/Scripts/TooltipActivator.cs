using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipActivator : MonoBehaviour
{
    public string description;

    private void OnMouseEnter()
    {
        ShowTooltip();
    }

    private void OnMouseExit()
    {
        HideTooltip();
    }

    public void ShowTooltip()
    {
        Tooltip.Instance.ShowTooltip(description);
    }

    public void HideTooltip()
    {
        Tooltip.Instance.HideTooltip();
    }
}
