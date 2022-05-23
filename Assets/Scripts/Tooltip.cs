using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] GameObject heartPanel;
    [SerializeField] Vector3 offset = Vector3.zero;


    private void Start()
    {
        if (label == null)
        {
            label = GetComponentInChildren<TextMeshProUGUI>();
        }

        HideTooltip();
    }

    private void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    public void HideTooltip()
    {
        label.text = "";
        gameObject.SetActive(false);
        heartPanel.SetActive(false);
    }

    public void ShowTooltip(string description)
    {
        if (!string.IsNullOrEmpty(description)) 
        {
            label.text = description;
            gameObject.SetActive(true);
        }
    }

    public void ShowTooltip(string description, int numOfHearts)
    {
        if (!string.IsNullOrEmpty(description))
        {
            label.text = description;
            gameObject.SetActive(true);
        }

        ShowHearts(numOfHearts);
    }

    public void ShowHearts(int numOfHearts)
    {
        heartPanel.SetActive(true);

        int count = 0;
        foreach (Transform heart in heartPanel.transform)
        {
            if (count < numOfHearts)
                heart.gameObject.SetActive(true);
            else
                heart.gameObject.SetActive(false);
            count++;
        }
    }
}
