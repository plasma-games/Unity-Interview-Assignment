using UnityEngine;
using System.Collections;

public class PanelContainer : MonoBehaviour
{
    [SerializeField] private GameSettingsObject gameSettings;
    [SerializeField] private Transform panelParent;
    [SerializeField] private WirePanel easyPanel;
    [SerializeField] private WirePanel mediumPanel;
    [SerializeField] private WirePanel hardPanel;
    [SerializeField] private Animator animator;

    private const string ANIMATION_TRIGGER_APPEAR = "appear";

    private void Start()
    {
        switch (gameSettings.difficulty)
        {
            case Difficulty.Easy:
                Instantiate(easyPanel, panelParent, false);
                break;
            case Difficulty.Medium:
                Instantiate(mediumPanel, panelParent, false);
                break;
            case Difficulty.Hard:
                Instantiate(hardPanel, panelParent, false);
                break;
        }
    }

    public void Appear()
    {
        animator.SetTrigger(ANIMATION_TRIGGER_APPEAR);
    }
}

