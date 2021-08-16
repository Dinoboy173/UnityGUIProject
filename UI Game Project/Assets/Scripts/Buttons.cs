using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Canvas canvas = null;

    public void MenuPlay()
    {
        Debug.Log("Play");

        SceneManager.LoadScene(sceneName: "LevelSelect");
    }

    public void MenuExit()
    {
        Debug.Log("Exit");

        Application.Quit();
    }

    public void LevelSelectBack()
    {
        Debug.Log("Back");

        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void LevelSelectOne()
    {
        Debug.Log("Level1");

        SceneManager.LoadScene(sceneName: "Level1");
    }

    public void LevelSelectTwo()
    {
        Debug.Log("Level2");

        SceneManager.LoadScene(sceneName: "Level2");
    }

    public void LevelSelectThree()
    {
        Debug.Log("Level3");

        SceneManager.LoadScene(sceneName: "Level3");
    }

    public void LevelSelectFour()
    {
        Debug.Log("Level4");

        SceneManager.LoadScene(sceneName: "Level4");
    }

    public void LevelSelectFive()
    {
        Debug.Log("Level5");

        SceneManager.LoadScene(sceneName: "Level5");
    }

    public void LevelSelectSix()
    {
        Debug.Log("Level6");

        SceneManager.LoadScene(sceneName: "Level6");
    }

    public void GameGo()
    {
        Debug.Log("Go");

        MainFuncControl actionListManager = GetComponentInParent<MainFuncControl>();

        if (actionListManager)
        {
            actionListManager.UpdatePlayerCommandList();
        }
    }

    public void GameReset()
    {
        Debug.Log("Reset");

        var currentScene = SceneManager.GetActiveScene();

        SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        SceneManager.LoadScene(currentScene.name);
    }

    public void GamePause()
    {
        Debug.Log("Pause");

        Time.timeScale = 0;

        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = null;

        canvas = GameObject.FindGameObjectWithTag("PauseGUI").GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.Log("Canvas Not Found");
        }

        canvas.enabled = true;
    }

    public void PauseReturn()
    {
        Debug.Log("Unpause");

        Time.timeScale = 1;

        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = null;

        canvas = GameObject.FindGameObjectWithTag("GameGUI").GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.Log("Not Found");
        }

        canvas.enabled = true;
    }

    public void PauseLeave()
    {
        Debug.Log("Leave");

        var currentScene = SceneManager.GetActiveScene();

        //PauseReturn();
        //GameReset();

        SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}