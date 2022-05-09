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

    private void OnDisable()
    {
        HideTooltip();
    }

    public void ShowTooltip()
    {
        if (GameManager.Instance.tooltip)
        {
            GameManager.Instance.tooltip.ShowTooltip(description);
        }
    }

    public void HideTooltip()
    {
        if (GameManager.Instance.tooltip)
        {
            GameManager.Instance.tooltip.HideTooltip();
        }
    }
}
