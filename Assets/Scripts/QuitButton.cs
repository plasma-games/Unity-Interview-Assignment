using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "Menu";

    public void QuitGame()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
}

