using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;
using System;

public class WireController : MonoBehaviour {

    public TextAsset jsonFile;
    public GameObject wirePrefab; 
    public int questionCounter; // 4 questions
    public Transform wireParent;
    public Canvas myCanvas;

    void Start() {
        Questions questionsInJson = JsonUtility.FromJson<Questions>(jsonFile.text);

        for (int i = 0; i < questionCounter; i++ ) {
            setQuestionAnswerText(
                questionsInJson.questions[i].text, 
                questionsInJson.questions[i].expectedAnswer, 
                i, 
                questionCounter
            );
        }

    }

    private void setQuestionAnswerText(string q, string a, int position, int totalQ) {
        // Create the two prefabs
        GameObject question = Instantiate(wirePrefab);
        GameObject answer = Instantiate(wirePrefab);

        question.transform.SetParent(wireParent);
        answer.transform.SetParent(wireParent);

        // Set the text to the correct question and answer pair
        TMP_Text qText = question.GetComponentInChildren<TMP_Text>();
        TMP_Text aText = answer.GetComponentInChildren<TMP_Text>();
        qText.text = q;
        aText.text = a;

        float canvasHeight = Screen.height; // myCanvas.GetComponent<RectTransform>().rect.height;
        float canvasWidth = Screen.width; //myCanvas.GetComponent<RectTransform>().rect.width;
        // you can also get the x and the y

        float verticalSlice = canvasHeight / (totalQ + 1); // + for space above and below
        float yAxis = (position - (totalQ / 2)) * verticalSlice; // remember our camera is centered on (0, 0, 0) // note we need to add .5 but ints v double v floats makes it hard so whatev4now
        float horizontalSlice = canvasWidth / 3;

        question.transform.position = new Vector3((1 * horizontalSlice), yAxis + (verticalSlice/2), 0) + wireParent.position; //  ?
        answer.transform.position = new Vector3((-1 * horizontalSlice), yAxis + (verticalSlice/2), 0) + wireParent.position;

        question.transform.localScale += new Vector3(10f, 10f, 10f);
        answer.transform.localScale += new Vector3(10f, 10f, 10f);
    }

}


