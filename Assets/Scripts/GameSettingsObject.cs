using System;
using UnityEngine;

[Serializable]
public enum Difficulty
{
    None = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3
}


[CreateAssetMenu (fileName = "SettingsObject", menuName = "GameSettingsObject")]
public class GameSettingsObject : ScriptableObject
{
    public Difficulty difficulty;
}
