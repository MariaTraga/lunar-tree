using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenTint : MonoBehaviour
{
    [SerializeField] Color untintedColor;
    [SerializeField] Color tintedColor;
    public float tintSpeed = 0.5f;

    float alphaCounter;

    Image image;

    private void Awake()
    {
        alphaCounter = 0f;
        image = GetComponent<Image>();
    }

    public void Tint(bool tint)
    {
        StopAllCoroutines();
        alphaCounter = 0f;
        StartCoroutine(HandleTintScreen(tint));
    }

    private IEnumerator HandleTintScreen(bool tint)
    {
        while (alphaCounter < 1f)
        {
            alphaCounter += Time.deltaTime * tintSpeed;
            alphaCounter = Mathf.Clamp(alphaCounter, 0f, 1f);
            Color c = image.color;

            if (tint)
                c = Color.Lerp(untintedColor, tintedColor, alphaCounter);
            else
                c = Color.Lerp(tintedColor, untintedColor, alphaCounter);

            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }
}
