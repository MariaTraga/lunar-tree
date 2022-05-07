using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightActivator : MonoBehaviour
{
    [SerializeField] Sprite lightOn;
    [SerializeField] Sprite lightOff;

    Light2D light2D;

    private void Awake()
    {
        light2D = transform.GetComponentInChildren<Light2D>();
    }

    public void ActivateLight(bool activeLight)
    {
        if (activeLight==true)
        {
            light2D.gameObject.SetActive(activeLight);
            transform.GetComponent<SpriteRenderer>().sprite = lightOn;
        }
        else
        {
            light2D.gameObject.SetActive(activeLight);
            transform.GetComponent<SpriteRenderer>().sprite = lightOff;
        }
    }
}
