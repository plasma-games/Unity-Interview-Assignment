using System;
using UnityEngine;

[Serializable]
public enum Difficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

// GameSettingsObject is a scriptable object that stores information about the
// game settings. Using scriptable objects is an easy way to transfer data between scenes.
[CreateAssetMenu (fileName = "SettingsObject", menuName = "GameSettingsObject")]
public class GameSettingsObject : ScriptableObject
{
    public Difficulty difficulty;
    public bool randomizeQuestionOrder;
}
