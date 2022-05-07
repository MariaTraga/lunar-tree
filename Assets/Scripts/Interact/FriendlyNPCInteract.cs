using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNPCInteract : Interactable
{
    [SerializeField] DialogueContainerObject dialogue;

    public override string Interact(Character character)
    {
        GameManager.Instance.dialogueController.Init(dialogue);
        return null;
    }

    public override void Highlight(bool activeHighlight)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Highlighter"))
            {
                child.gameObject.SetActive(activeHighlight);
            }
        }
    }
}
