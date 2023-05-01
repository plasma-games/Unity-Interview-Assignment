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

    public void SetQuestion(LevelQuestion _question, Color questionColor)
    {
        question = _question;
        label.text = question.text;
        startImage.color = questionColor;
        orb.Initialize(questionColor, question.expectedAnswer);
    }
}