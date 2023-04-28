using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PanelContainer panelContainer;

    private const string ANIMATION_TRIGGER_SHRINK = "shrink";

    public void BeginLevel()
    {
        animator.SetTrigger(ANIMATION_TRIGGER_SHRINK);
        panelContainer.Appear();
    }

}
