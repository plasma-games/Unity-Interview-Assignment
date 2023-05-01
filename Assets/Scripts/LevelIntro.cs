using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private Animator animator;
    [SerializeField] private PanelContainer panelContainer;
    [SerializeField] private float appearDelay;

    private const string ANIMATION_TRIGGER_GROW = "grow";
    private const string ANIMATION_TRIGGER_SHRINK = "shrink";

    private void Start()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(appearDelay);
        animator.SetTrigger(ANIMATION_TRIGGER_GROW);
    }

    public void BeginLevel()
    {
        soundManager.PlayClip(buttonSound);
        animator.SetTrigger(ANIMATION_TRIGGER_SHRINK);
        panelContainer.Appear();
    }

}
