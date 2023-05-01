using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        titleText.text = didWin ? winTitle : loseTitle;
        messageText.text = string.Format(messageText.text, numCorrect, total);
        winMessageObject.SetActive(didWin);
        loseMessageObject.SetActive(!didWin);

        foreach(PulsingLight light in lights)
        {
            light.ShowEndLevel(didWin);
        }

        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(appearDelay);
        animator.SetTrigger(ANIMATION_TRIGGER_GROW);
    }
}

