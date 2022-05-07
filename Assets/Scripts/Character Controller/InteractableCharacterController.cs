using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacterController : MonoBehaviour
{
    CharacterController2D characterController;
    Character character;
    DialogueController dialogueController;
    string animationString;

    [Header("Object Interact Area Properties")]
    [SerializeField] float offsetDistance = 0.8f;
    [SerializeField] float sizeOfInteractableArea = 0.4f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        character = GetComponent<Character>();
        dialogueController = GameManager.Instance.dialogueController;
    }

    void Update()
    {
        //Break if dialogue started in order to not start a new conversation on every button press.
        HandleDialogue();

        if (dialogueController.isTalking == true)
        {
            return;
        }

        if (/*Input.GetKeyDown(KeyCode.Mouse0) ||*/ Input.GetKeyDown(KeyCode.Return))
        {
            InteractObject();
        }
    } 
    
    //INTERACT
    private void InteractObject()
    {
        //Create interactable area collider
        Collider2D[] interactColliders = characterController.CreateInteractArea(offsetDistance, sizeOfInteractableArea);

        //Interact with object
        foreach (Collider2D interactCollider in interactColliders)
        {
            Interactable interactable = interactCollider.GetComponent<Interactable>();
            if (interactable != null)
            {
                animationString = interactable.Interact(character);
                //Handle animation if it exists
                if (animationString != null)
                {
                    GetComponentInChildren<Animator>().SetTrigger(animationString);
                }
                break;
            }
        }
    }

    private void HandleDialogue()
    {
        if(dialogueController.isTalking == true)
        {
            return;
        }
    }


    //HIGHLIGHTER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Find if collision object sah Interactable (base class) component
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable)
        {
            interactable.Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        if (interactable)
        {
            interactable.Highlight(false);
        }
    }
}
