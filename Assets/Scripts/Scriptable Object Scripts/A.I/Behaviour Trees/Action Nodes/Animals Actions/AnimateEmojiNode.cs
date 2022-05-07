using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEmojiNode : ActionNode
{
    [SerializeField] string animationString = "";

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
        return NodeState.SUCCESS;
    }
}
