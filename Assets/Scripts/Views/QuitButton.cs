using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// The QuitButton handles moving from the game scene back to the menu scene.
public class QuitButton : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float transitionTime;
    [SerializeField] private Image overlay;

    private const string MENU_SCENE_NAME = "Menu";

    public void QuitGame()
    {
        soundManager.PlayClip(buttonSound);
        StartCoroutine(TransitionToMenu());
    }

    private IEnumerator TransitionToMenu()
    {
        StartCoroutine(soundManager.FadeOutMusic(transitionTime));

        overlay.enabled = true;
        while (overlay.color.a < 1)
        {
            float alpha = overlay.color.a + (Time.deltaTime / transitionTime);
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
}

