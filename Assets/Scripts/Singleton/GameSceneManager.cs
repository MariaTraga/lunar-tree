using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager _instance;

    public static GameSceneManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] TransitionScreenTint transitionScreenTint;
    [SerializeField] CameraConfinerController cameraConfinerController;

    string currentScene;
    CharacterController2D playerMovement;
    AsyncOperation load,unload;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        playerMovement = GameManager.Instance.player.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("exited");
            Application.Quit();
        }
    }

    public void InitSwitchScene(string sceneName, Vector3 targetPosition, VolumeProfile volumeProfile, AudioClip walkSound)
    {
        StartCoroutine(HandleTransition(sceneName, targetPosition, volumeProfile, walkSound));
    }

    IEnumerator HandleTransition(string sceneName, Vector3 targetPosition, VolumeProfile volumeProfile, AudioClip walkSound)
    {
        HandleMovementOnTransition(false);
        transitionScreenTint.Tint(true);

        yield return new WaitForSeconds(1f / transitionScreenTint.tintSpeed + 0.1f);

        SwitchScene(sceneName,targetPosition,volumeProfile, walkSound);
        
        while (load != null & unload != null)
        {
            if (load.isDone)
                load = null;
            if (unload.isDone)
                unload = null;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfinerController.UpdateBounds();
        transitionScreenTint.Tint(false);
        HandleMovementOnTransition(true);
    }

    public void SwitchScene(string sceneName, Vector3 targetPosition, VolumeProfile volumeProfile, AudioClip walkSound)
    {
        load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneName;

        Transform playerTransform = GameManager.Instance.player.transform;

        Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

        GameManager.Instance.player.transform.position = targetPosition;
        GameManager.Instance.globalVolume.profile = volumeProfile;
        GameManager.Instance.player.GetComponent<CharacterController2D>().walkSound = walkSound;
    }

    private void HandleMovementOnTransition(bool isMoving)
    {
        if (isMoving == false)
        {
            playerMovement.StopMoving();
            playerMovement.enabled = isMoving;
        }
        else
        {
            playerMovement.enabled = isMoving;
            playerMovement.Move();
        }
    }
}
