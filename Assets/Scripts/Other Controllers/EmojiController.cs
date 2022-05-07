using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AnimateBubble(string bubbleString)
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetTrigger(bubbleString);
    }

    public void EmptyBubble()
    {
        spriteRenderer.enabled = false;
    }

}
