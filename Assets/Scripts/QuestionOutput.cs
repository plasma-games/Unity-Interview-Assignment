using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionOutput : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image endImage;

    public void SetLabelText(string labelText)
    {
        label.text = labelText;
    }
}

