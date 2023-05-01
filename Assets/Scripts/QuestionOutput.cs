using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionOutput : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image endImage;

    public string answer { get; private set; }

    public void SetLabelText(string labelText)
    {
        answer = labelText;
        label.text = answer;
    }

    public void SetColor(Color newColor)
    {
        endImage.color = newColor;
    }
}

