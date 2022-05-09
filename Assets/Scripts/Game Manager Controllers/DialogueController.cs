using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] GameObject dialogueUICanvas;
    [SerializeField] GameObject toolbarUICanvas;
    [SerializeField] TextMeshProUGUI targetDialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image portraitImage;
    [SerializeField] float dialogueExitRange = 3f;

    Vector2 dialogueStartingPoint;
    DialogueContainerObject currentDialogue;
    int currentTextLine;
    public bool isTalking = false;

    

    private void Update()
    {
        if(dialogueUICanvas.activeInHierarchy == false) { return; }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            Debug.Log(currentTextLine);
            PushText();
        }

        if (Vector2.Distance(GameManager.Instance.player.transform.position, dialogueStartingPoint) >= dialogueExitRange)
        {
            ConcludeDialogue();
        }

    }

    private void PushText()
    {
        currentTextLine += 1;
        if(currentTextLine >= currentDialogue.lines.Count)
        {
            ConcludeDialogue();
        }
        else
        {
            targetDialogueText.text = currentDialogue.lines[currentTextLine];  
        }
    }

    private void UpdateActorDetails()
    {
        portraitImage.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.actorName;
    }

    public void Init(DialogueContainerObject dialogueContainer)
    {
        Show(true);
        isTalking = true;
        currentDialogue = dialogueContainer;
        currentTextLine = 0;
        targetDialogueText.text = currentDialogue.lines[currentTextLine];
        UpdateActorDetails();

        dialogueStartingPoint = GameManager.Instance.player.transform.position;
    }

    private void Show(bool isActive)
    {
        dialogueUICanvas.SetActive(isActive);
        toolbarUICanvas.SetActive(!isActive);
    }

    private void ConcludeDialogue()
    {
        Debug.Log("Conversation ended.");
        Show(false);
        isTalking = false;
    }
}
