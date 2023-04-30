using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WirePanel : MonoBehaviour
{
    [SerializeField] private TextAsset levelDefinitionJSON;
    [SerializeField] private QuestionInput[] inputs;
    [SerializeField] private QuestionOutput[] outputs;
    [SerializeField] private Color[] questionColors;

    private LevelDefinition levelDefinition;

    public void Initialize(GameSettingsObject gameSettings)
    {
        if (inputs.Length != outputs.Length)
        {
            string errorMessage = string.Format("The panel has {0} inputs and {1} outputs. " +
                "Panels must have an equal number of inputs and outputs.", inputs.Length, outputs.Length);
            Debug.LogError(errorMessage);
        }

        questionColors.Shuffle();

        levelDefinition = JsonUtility.FromJson<LevelDefinition>(levelDefinitionJSON.text);

        LevelQuestion[] activeQuestions = SelectQuestions(levelDefinition, inputs.Length);

        if (activeQuestions.Length != questionColors.Length)
        {
            string errorMessage = string.Format("The panel has {0} questions and {1} colors. " +
                "There must be an equal number of questions and colors.", activeQuestions.Length, questionColors.Length);
            Debug.LogError(errorMessage);
        }

        PopulatePanel(activeQuestions, gameSettings);
    }

    private void PopulatePanel(LevelQuestion[] activeQuestions, GameSettingsObject gameSettings)
    {
        if (gameSettings.randomizeQuestionOrder)
        {
            activeQuestions.Shuffle();
        }
        else
        {
            activeQuestions = activeQuestions.OrderBy(q => q.questionNumber).ToArray();
        }

        foreach (LevelQuestion question in activeQuestions)
        {
            Debug.Log("selected question: " + question.text+":"+question.questionNumber);
        }

        List<string> answers = new List<string>();

        foreach(LevelQuestion question in activeQuestions)
        {
            answers.Add(question.expectedAnswer);
        }

        answers.Shuffle();

        for (int i = 0; i < activeQuestions.Length; i++)
        {
            inputs[i].SetQuestion(activeQuestions[i], questionColors[i]);
            outputs[i].SetLabelText(answers[i]);
        }
    }

    private LevelQuestion[] SelectQuestions(LevelDefinition levelDefinition, int numQuestions)
    {
        if (levelDefinition.questions.Length < numQuestions)
        {
            string errorMessage = string.Format("Error! Unable to populate panel. " +
                "Panel requires {0} questions but level defintion only contains {1}.",
                numQuestions, levelDefinition.questions.Length);
            Debug.LogError(errorMessage);
        }
        else if (levelDefinition.questions.Length == numQuestions)
        {
            return levelDefinition.questions;
        }

        List<LevelQuestion> questionPool = new List<LevelQuestion>();

        foreach (LevelQuestion question in levelDefinition.questions)
        {
            for (int i = 0; i < question.weight; i++)
            {
                questionPool.Add(question);
            }
        }

        questionPool.Shuffle();

        foreach(LevelQuestion q in questionPool)
        {
            Debug.Log(q.text);
        }

        LevelQuestion[] activeQuestions = new LevelQuestion[numQuestions];

        for (int i = 0; i < numQuestions; i++)
        {
            LevelQuestion nextQuestion = questionPool[0];
            questionPool.RemoveAll(q => q.text == nextQuestion.text);

            activeQuestions[i] = nextQuestion;
        }

        return activeQuestions;
    }

    public void CheckAnswers()
    {
        int numCorrect = 0;
        int total = inputs.Length;

        foreach(QuestionInput input in inputs)
        {
            if (input.isCorrect)
            {
                numCorrect++;
            }
        }

        Debug.Log(numCorrect + ":" + total);
    }
}

