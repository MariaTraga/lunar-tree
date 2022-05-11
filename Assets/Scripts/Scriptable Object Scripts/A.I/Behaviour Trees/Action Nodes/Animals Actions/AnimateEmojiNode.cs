using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEmojiNode : ActionNode
{
    enum EmojiType
    {
        Happy,
        Angry
    }

    [SerializeField] EmojiType emojiType;
    [SerializeField] string animationString = "";
    //[SerializeField] AudioClip audioClip;
    [SerializeField] float audioCooldown = 10f;
    float audioCooldownStartTime = 0f;

    EmojiController emojiController;
    AudioClip audioClipHappy, audioClipAngry;

    protected override void OnStart()
    {
        emojiController = owner.GetComponentInChildren<EmojiController>();
        audioClipHappy = owner.GetComponent<AnimalController>().animalObject.happySound;
        audioClipAngry = owner.GetComponent<AnimalController>().animalObject.angrySound;
    }

    protected override void OnStop()
    {
        //emojiController.EmptyBubble();
    }

    protected override NodeState OnUpdate()
    {
        emojiController.AnimateBubble(animationString);

        if(audioClipHappy != null && audioClipAngry != null)
        {
            if (Time.time - audioCooldownStartTime >= audioCooldown)
            {
                switch (emojiType)
                {
                    case EmojiType.Happy:
                        AudioManager.Instance.Play(audioClipHappy);
                        break;
                    case EmojiType.Angry:
                        AudioManager.Instance.Play(audioClipAngry);
                        break;
                }

                audioCooldownStartTime = Time.time;
            }
        }
       
        return NodeState.SUCCESS;
    }
}
