using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsController : MonoBehaviour
{
    [Header("Day Night Cycle Graphics Presentation")]
    [SerializeField] GameObject[] lightsOverworld;
    //TODO Firefly sprites SpriteRenderer fireflySprites;

    int hh;
    List<Light2D> lights2D;

    private void Awake()
    {
        lights2D = new List<Light2D>();
        foreach (GameObject lightOverworld in lightsOverworld)
        {
            lights2D.Add(lightOverworld.GetComponentInChildren<Light2D>());
        }
    }

    void Start()
    {
        hh = (int)GameManager.Instance.timeController.Hours;
 
    }

    void Update()
    {
        hh = (int)GameManager.Instance.timeController.Hours;
        HandleLights();
    }

    private void HandleLights()
    {

        if (hh >= 5 && hh < 21)
        {
            ActivateLights(false);
        }
        else
        {
            ActivateLights(true);
        }
    }

    private void ActivateLights(bool activeLights)
    {
        foreach (GameObject lightOverworld in lightsOverworld)
        {
            lightOverworld.GetComponent<LightActivator>().ActivateLight(activeLights);
        }

        /*foreach (Light2D light in lights2D)
        {
            light.gameObject.SetActive(activeLights);

        }*/
    }

    
}
