using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// The WirePanel contains inputs and outputs - one for each question in the level.
// Once the panel has been set up, it waits for the user to hit the Engage button
// then it counts the number of correct answers and triggers the end level sequence.
public class WirePanel : MonoBehaviour
{
    [SerializeField] private TextAsset levelDefinitionJSON;
    [SerializeField] private QuestionInput[] inputs;
    [SerializeField] private QuestionOutput[] outputs;
    [SerializeField] private Color[] questionColors;

    private LevelDefinition levelDefinition;
    private PanelContainer panelContainer;

    public void Initialize(GameSettingsObject gameSettings, SoundManager soundManager, PanelContainer _panelContainer)
    {
        if (inputs.Length != outputs.Length)
        {
            string errorMessage = string.Format("The panel has {0} inputs and {1} outputs. " +
                "Panels must have an equal number of inputs and outputs.", inputs.Length, outputs.Length);
            Debug.LogError(errorMessage);
        }

        if (inputs.Length != questionColors.Length)
        {
            string errorMessage = string.Format("The panel has {0} inputs and {1} colors. " +
                "There must be an equal number of inputs and colors.", inputs.Length, questionColors.Length);
            Debug.LogError(errorMessage);
        }

        panelContainer = _panelContainer;

        // Mix up the order of the colors so every level is different
        questionColors.Shuffle();

        // Convert the level's json definition into a LevelDefinition object
        levelDefinition = JsonUtility.FromJson<LevelDefinition>(levelDefinitionJSON.text);

        // Determine which questions from the complete set will be used in this level
        LevelQuestion[] activeQuestions = SelectQuestions(levelDefinition, inputs.Length);

        // Fill in the panel with the chosen questions
        PopulatePanel(activeQuestions, gameSettings, soundManager);
    }

    private void PopulatePanel(LevelQuestion[] activeQuestions, GameSettingsObject gameSettings, SoundManager soundManager)
    {
        // The json question sets include an explicit question order. It may be
        // useful to always show the questions in that order, but we also want
        // the option to randomize the question order.
        if (gameSettings.randomizeQuestionOrder)
        {
            activeQuestions.Shuffle();
        }
        else
        {
            activeQuestions = activeQuestions.OrderBy(q => q.questionNumber).ToArray();
        }

        // Create a list of the answers to the questions and randomize their order
        List<string> answers = new List<string>();

        foreach (LevelQuestion question in activeQuestions)
        {
            answers.Add(question.expectedAnswer);
        }

        answers.Shuffle();

        // Set up each of the inputs and outputs with the questions and answers
        for (int i = 0; i < activeQuestions.Length; i++)
        {
            inputs[i].Initialize(activeQuestions[i], questionColors[i], soundManager);
            outputs[i].SetLabelText(answers[i]);
        }
    }

    // The method for selecting questions is sufficiently flexible so that if the
    // question set has more options than there are inputs for this level, then it
    // randomly chooses a subset of questions according to their weights.
    // Questions with higher weight are more likely to be selected.
    //
    // The "easy_questions" set demonstrates this approach by having four available
    // questions for only two inputs. The first question is heavily weighted
    // and is therefore very likely to appear in the level.
    private LevelQuestion[] SelectQuestions(LevelDefinition levelDefinition, int numQuestions)
    {
        // There must be at least enough questions for each input in this level.
        if (levelDefinition.questions.Length < numQuestions)
        {
            string errorMessage = string.Format("Error! Unable to populate panel. " +
                "Panel requires {0} questions but level defintion only contains {1}.",
                numQuestions, levelDefinition.questions.Length);
            Debug.LogError(errorMessage);
        }
        // If the number of questions in the set matches the number of inputs,
        // simply use all questions from the set.
        else if (levelDefinition.questions.Length == numQuestions)
        {
            return levelDefinition.questions;
        }

        // If there are more questions than inputs, create a pool of possible questions
        // where each question is added to the pool a number of times equal to its
        // weight. This way questions with higher weight are more likely to be selected.
        List<LevelQuestion> questionPool = new List<LevelQuestion>();

        foreach (LevelQuestion question in levelDefinition.questions)
        {
            for (int i = 0; i < question.weight; i++)
            {
                questionPool.Add(question);
            }
        }

        questionPool.Shuffle();

        LevelQuestion[] activeQuestions = new LevelQuestion[numQuestions];

        // Go through the pool of possible questions, selecting the first element
        // from the shuffled list, then removing all duplicates of that selected
        // question. 
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
        int numCorrect = inputs.Where(i => i.IsCorrect).Count();

        panelContainer.ShowEndScreen(numCorrect, inputs.Length);
    }
}