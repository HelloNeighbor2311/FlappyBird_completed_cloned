using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public enum SceneEnum
    {
        MainMenu,
        GamePlayScene
    }

    public static void LoadScene(SceneEnum sceneEnum)
    {
        SceneManager.LoadScene(sceneEnum.ToString());
    }
}
