using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        // wow they really did using management to load manager
        SceneManager.LoadScene("level1");
    }

    public void QuitGame() {
        // Debug.Log("The game is quitting");
        Application.Quit();    
    }

}
