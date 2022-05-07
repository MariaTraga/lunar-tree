using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITooltipInteract
{
    public string description { get; }
}

public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;

    bool isActive = false;

    private void Start()
    {
        if (!label)
        {
            label = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newPos = new Vector3(mousePos.x, mousePos.y, 0);
        gameObject.transform.position = newPos;

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, newPos);
        Debug.DrawRay(Camera.main.transform.position, newPos, Color.red, 0.2f);
        // If it hits something...
        if (hit)
        {
            var gOTooltip = hit.collider.gameObject.GetComponent<ITooltipInteract>();
            if (gOTooltip != null)
            {
                ShowTooltip(gOTooltip.description);
                Debug.Log(gOTooltip.description);
            }
            else
            {
                Debug.Log("No interaction");
            }
        }
        else
        {
            HideTooltip();
        }
    }

    private void HideTooltip()
    {
        label.text = "";
    }

    private void ShowTooltip(string description)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        label.text = description;
    }
}
