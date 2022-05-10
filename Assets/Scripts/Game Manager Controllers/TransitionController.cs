using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum TransitionType
{
    Warp,
    Scene
}

public class TransitionController : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] AudioClip walkSound;
 
    Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        if (transitionType != TransitionType.Scene)
        {
            destination = transform.GetComponentInChildren<Destination>().transform;
        }
    }

    internal void InitiateTransition(Transform transitioningObject)
    {

        switch (transitionType)
        {
            case TransitionType.Warp:
                Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(transitioningObject, destination.position - transitioningObject.position);

                transitioningObject.position = destination.position;

                break;

            case TransitionType.Scene:
                
                GameSceneManager.Instance.InitSwitchScene(sceneNameToTransition, targetPosition, volumeProfile, walkSound);
                break;
        }

        
    }

}
