using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEmojiNode : ActionNode
{
    [SerializeField] string animationString = "";
    [SerializeField] AudioClip audioClip;
    [SerializeField] float audioCooldown = 10f;
    float audioCooldownStartTime = 0f;

    EmojiController emojiController;

    protected override void OnStart()
    {
        emojiController = owner.GetComponentInChildren<EmojiController>();
    }

    protected override void OnStop()
    {
        //emojiController.EmptyBubble();
    }

    protected override NodeState OnUpdate()
    {
        emojiController.AnimateBubble(animationString);

        if(Time.time - audioCooldownStartTime >= audioCooldown)
        {
            AudioManager.Instance.Play(audioClip);
            audioCooldownStartTime = Time.time;
        }
        
        return NodeState.SUCCESS;
    }
}
