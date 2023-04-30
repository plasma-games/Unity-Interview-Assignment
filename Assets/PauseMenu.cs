using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void MainMenu() {
        SceneManager.LoadScene("menu");
    }

    public void WireGame() {
        SceneManager.LoadScene("level1");
    }
}
