using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    //call this method to switch to another scene
    public void SwitchScene(string sceneName) => SceneManager.LoadScene(sceneName);  
}