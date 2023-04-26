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
    [SerializeField] private GameObject wireStartParent, wireEndParent;
    public string questionsFileName = "questions";
    private List<Question> questionsList;
    private int numberOfQuestions, questionsAnswered, correctAnswers, incorrectAnswers;
    //using a dictionary in place of string comparisons will be more efficient, so we will map Questions and Answers to the dict
    private Dictionary<string, string> questionAnswerMap = new Dictionary<string, string>();

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(questionsFileName);
        QuestionData data = JsonUtility.FromJson<QuestionData>(jsonFile.text);
        questionsList = new List<Question>(data.questions);
        //get number of questions for tracking when we have answered them all
        numberOfQuestions = data.questions.Length;
        AssignQA();
    }

    private void AssignQA()
    {
        for (int i = 0; i < wireStartParent.transform.childCount; i++)
        {
            Transform childObj = wireStartParent.transform.GetChild(i);
            TextMeshProUGUI textmp = childObj.GetComponentInChildren<TextMeshProUGUI>();
            string expectedAnswer = questionsList[i].expectedAnswer;
            string questionText = questionsList[i].text;
            textmp.text = expectedAnswer;
            // Add the question and expected answer pair to the dictionary
            questionAnswerMap.Add(questionText.ToLower(), expectedAnswer.ToLower());
            //here we assign the same question text to the wire script for answer checking later on
            childObj.GetComponentInChildren<WireScript>().answerText = expectedAnswer;
        }
        for (int i = 0; i < wireEndParent.transform.childCount; i++)
        {
            Transform childObj = wireEndParent.transform.GetChild(i);
            TextMeshProUGUI textmp = childObj.GetComponentInChildren<TextMeshProUGUI>();
            textmp.text = questionsList[i].text;
        }
    }


    public void ProcessAnswer(string questionText, string answer)
    {
        if(IsAnswerCorrect(questionText, answer) == true)
        {
            questionsAnswered++;
            correctAnswers++;
        }
        else
        {
            questionsAnswered++;
            incorrectAnswers++;
        }

        if(numberOfQuestions == questionsAnswered)
        {
            Debug.Log("Done!");
            Debug.Log("Correct: " + correctAnswers);
            Debug.Log("Incorrect: " + incorrectAnswers);
        }
    }

    private bool IsAnswerCorrect(string questionText, string answer)
    {
        // Check if the question text is present in the dictionary and the answer matches the expected answer
        if (questionAnswerMap.ContainsKey(questionText.ToLower()) && questionAnswerMap[questionText.ToLower()] == answer.ToLower())
        {
            return true;
        }
        return false;
    }
}