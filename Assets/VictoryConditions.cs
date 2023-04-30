using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditions : MonoBehaviour
{
    static public VictoryConditions Instance;

    public int totalQuestions;
    public GameObject winText;
    private int onCount = 0; // how many wires connected / questions answered

    private void Awake() {
        Instance = this;
    }

    public void WireConnected(int points) {
        onCount = onCount + points;
        if(onCount == totalQuestions) {
            winText.SetActive(true);
        }
    }
}
