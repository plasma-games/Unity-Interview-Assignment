using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionInput : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image startImage;
    [SerializeField] private Image dragImage;

    private LevelQuestion question;
    public bool isCorrect { get; private set; }

    public void SetQuestion(LevelQuestion _question, Color questionColor)
    {
        question = _question;
        label.text = question.text;
        startImage.color = questionColor;
        dragImage.color = questionColor;
    }

    public void AnswerSelected(string answerText)
    {
        isCorrect = answerText.Equals(question.expectedAnswer);
    }
}

