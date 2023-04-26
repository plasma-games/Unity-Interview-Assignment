using UnityEngine;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class QuestionData
{
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public int questionNumber;
    public string text;
    public string expectedAnswer;
    public int weight;
}

public class QAHandler : MonoBehaviour
{
    public string questionsFileName = "questions";

    private List<Question> questionsList;

    [SerializeField] private GameObject wireStartParent, wireEndParent;

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(questionsFileName);
        QuestionData data = JsonUtility.FromJson<QuestionData>(jsonFile.text);
        questionsList = new List<Question>(data.questions);
        AssignQA();
    }

    private void AssignQA()
    {
        for (int i = 0; i < wireStartParent.transform.childCount; i++)
        {
            Transform childObj = wireStartParent.transform.GetChild(i);
            TextMeshProUGUI textmp = childObj.GetComponentInChildren<TextMeshProUGUI>();
            textmp.text = questionsList[i].expectedAnswer;
        }
        for (int i = 0; i < wireEndParent.transform.childCount; i++)
        {
            Transform childObj = wireEndParent.transform.GetChild(i);
            TextMeshProUGUI textmp = childObj.GetComponentInChildren<TextMeshProUGUI>();
            textmp.text = questionsList[i].text;
        }
    }
}