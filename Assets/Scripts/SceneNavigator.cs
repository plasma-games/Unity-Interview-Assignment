using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNavigator 
{
    private const string MENU_SCENE_NAME = "Menu";
    private const string GAME_SCENE_NAME = "Game";

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
}
