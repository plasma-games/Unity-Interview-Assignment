using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float transitionTime;
    [SerializeField] private Image overlay;
    [SerializeField] private GameSettingsObject gameSettings;

    private const string GAME_SCENE_NAME = "Game";

    public void StartGame(int difficulty)
    {
        gameSettings.difficulty = (Difficulty) difficulty;
        soundManager.PlayClip(buttonSound);
        StartCoroutine(TransitionToGame());
    }

    public IEnumerator TransitionToGame()
    {
        StartCoroutine(soundManager.FadeOutMusic(transitionTime));

        overlay.enabled = true;
        while (overlay.color.a < 1)
        {
            float alpha = overlay.color.a + (Time.deltaTime / transitionTime);
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alpha);

            yield return null;
        }

        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
}
