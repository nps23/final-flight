using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneControl provides functionality for all scene management including loading new scenes, pausing, and 
// quiting the game
public class SceneControl : MonoBehaviour
{

    public void LoadSceneByName(string sceneName)
    {
        if (sceneName != "" && sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
