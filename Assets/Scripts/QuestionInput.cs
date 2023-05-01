using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionInput : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image startImage;
    [SerializeField] private EnergyOrb orb;

    public bool IsCorrect { get { return orb.isCorrect; } }

    private LevelQuestion question;

    public void Initialize(LevelQuestion _question, Color questionColor, SoundManager soundManager)
    {
        question = _question;
        label.text = question.text;
        startImage.color = questionColor;
        orb.Initialize(questionColor, question.expectedAnswer, soundManager);
    }
}