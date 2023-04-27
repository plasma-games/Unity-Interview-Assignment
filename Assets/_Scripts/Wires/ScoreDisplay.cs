using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public static ScoreDisplay instance;
    [SerializeField] private TextMeshProUGUI grade;
    [SerializeField] private TextMeshProUGUI sentence;

    //typical, basic way to keep the script and object into the next scene
    //there is more code typically used, to ensure that there is not a copy of
    //ScoreDisplay.cs in the next scene but this is a simple two scene project,
    //I would quickly add more verbose code if the project were to be larger than this
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //we pass on the correct and incorrect amounts from QAHandler before switching scenes to display the score
    public void UpdateScore(int correct, int incorrect)
    {
        //divide correct answers by total number of answers
        float tempGrade = ((float)correct / (correct + incorrect)) * 100;
        //I was going to do a grade letter scale but since the only amount of right answers is 1/4, 2/4, or 4/4 I used numbers instead
        sentence.text = $"You got {correct} answers right and {incorrect} answers wrong, your grade is: ";
        grade.text = $"{tempGrade} / 100";
    }     
}
