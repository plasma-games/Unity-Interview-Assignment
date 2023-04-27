using UnityEngine;
using System.Collections.Generic;
using TMPro;

//Setup for parsing JSON
//This should be scalable, but for the demonstration and from the gif provided,
//I am assuming a max of four questions and answers, although the script can mechanically accept more,
//The UI is not set up for this. If multiple questions were used, with the current art assets,
//I would probably reload the wires every four sets of questions, so as long as the JSON provided
//sets of questions that were a multiple of 4 I could quickly modify the script to handle more than 
//4 questions, alternatively I would plan to have a scrolling set of wires perhaps, that could hold
//more than 4 sets by using art that is scalable, perhaps by slicing up the provided backdrop that I cut from the gif.
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

//This script is called QAHandler but it more specifically processes the wire answers
//I would ideally create a more focused script for general question and answer handling,
//and if for this test I needed to handle the score or grading of two seperate games
//I would have likely made a parent class out of this script, and had more specific
//grading per minigame delegated into new scripts
public class QAHandler : MonoBehaviour
{
    [SerializeField] private GameObject wireStartParent, wireEndParent;
    public string questionsFileName = "questions";
    private List<Question> questionsList;
    private int numberOfQuestions, questionsAnswered, correctAnswers, incorrectAnswers;
    [SerializeField] private ScoreDisplay scoreDisplay;
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

    //Scoring for the wire game
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
            scoreDisplay.UpdateScore(correctAnswers, incorrectAnswers);
            //slight delay added to let particle effects be shown
            Invoke("LoadScoringScene", 2f);
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

    private void LoadScoringScene()
    {
        GetComponent<SceneSwitcher>().SwitchScene("ScoringScene");
    }
}