using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] string essentialSystemScene = "Essential Systems";
    [SerializeField] string newGameStartScene = "Overworld Scene";

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartDemo()
    {     
        SceneManager.LoadScene(newGameStartScene, LoadSceneMode.Single);
        SceneManager.LoadScene(essentialSystemScene, LoadSceneMode.Additive);
    }
}
