using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This script controls the dialog shown once the player has submitted their answers.
// It also triggers the background lights to stop pulsing and change colors if the
// player got every quesiton correct.
public class EndLevelScreen : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text messageText;
    [SerializeField] private Text buttonText;
    [SerializeField] private Animator animator;
    [SerializeField] private float appearDelay;
    [SerializeField] private PulsingLight[] lights;
    [SerializeField] private string winTitle;
    [SerializeField] private string loseTitle;
    [SerializeField] private GameObject winMessageObject;
    [SerializeField] private GameObject loseMessageObject;

    private const string ANIMATION_TRIGGER_GROW = "grow";

    public void ShowEndLevelScreen(int numCorrect, int total)
    { 
        bool didWin = numCorrect == total;

        // Set up the text according to whether or not the player won the level.
        titleText.text = didWin ? winTitle : loseTitle;
        messageText.text = string.Format(messageText.text, numCorrect, total);
        winMessageObject.SetActive(didWin);
        loseMessageObject.SetActive(!didWin);

        // Tell the background lights to stop pulsing.
        foreach(PulsingLight light in lights)
        {
            light.ShowEndLevel(didWin);
        }

        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        // We wait a bit to show this dialog to emphasize the lights changing.
        yield return new WaitForSeconds(appearDelay);
        animator.SetTrigger(ANIMATION_TRIGGER_GROW);
    }
}

