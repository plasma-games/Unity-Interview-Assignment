﻿using UnityEngine;
using System.Collections;

public class PanelContainer : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip engageSound;
    [SerializeField] private GameSettingsObject gameSettings;
    [SerializeField] private Transform panelParent;
    [SerializeField] private WirePanel easyPanel;
    [SerializeField] private WirePanel mediumPanel;
    [SerializeField] private WirePanel hardPanel;
    [SerializeField] private Animator animator;
    [SerializeField] private QuitButton quitButton;
    [SerializeField] private EndLevelScreen endLevelScreen;

    private WirePanel activePanel;

    private const string ANIMATION_TRIGGER_APPEAR = "appear";
    private const string ANIMATION_TRIGGER_DISAPPEAR = "disappear";

    private void Start()
    {
        if (gameSettings.difficulty == Difficulty.None)
        {
            Debug.LogWarning("Difficulty has not been set. Returning to main menu.");
            quitButton.QuitGame();
            return;
        }

        switch (gameSettings.difficulty)
        {
            case Difficulty.Easy:
                activePanel = Instantiate(easyPanel, panelParent, false);
                break;
            case Difficulty.Medium:
                activePanel = Instantiate(mediumPanel, panelParent, false);
                break;
            case Difficulty.Hard:
                activePanel = Instantiate(hardPanel, panelParent, false);
                break;
        }

        activePanel.Initialize(gameSettings, soundManager, this);
    }

    public void Appear()
    {
        animator.SetTrigger(ANIMATION_TRIGGER_APPEAR);
    }

    public void ShowEndScreen(int numCorrect, int total)
    {
        soundManager.PlayClip(engageSound);
        animator.SetTrigger(ANIMATION_TRIGGER_DISAPPEAR);
        endLevelScreen.ShowEndLevelScreen(numCorrect, total);
    }
}

