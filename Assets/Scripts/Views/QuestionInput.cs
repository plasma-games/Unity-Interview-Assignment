using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// The QuestionInput is primarily responsible for displaying a question's text
// and initializing its orb.
public class QuestionInput : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image baseImage;
    [SerializeField] private Image ringImage;
    [SerializeField] private EnergyOrb orb;

    public bool IsCorrect { get { return orb.isCorrect; } }

    private LevelQuestion question;

    public void Initialize(LevelQuestion _question, Color questionColor, SoundManager soundManager)
    {
        question = _question;
        label.text = question.text;
        baseImage.color = ringImage.color = questionColor;
        orb.Initialize(questionColor, question.expectedAnswer, soundManager);
    }
}