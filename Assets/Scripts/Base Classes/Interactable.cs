using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual string Interact(Character character)
    {
        return null;
    }

    public virtual void Highlight(bool activeHighlight)
    {

    }

    public virtual void Open(Character character)
    {

    }

    public virtual void Close(Character character)
    {

    }
}
