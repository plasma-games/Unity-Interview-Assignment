using System;

[Serializable]
public class LevelDefinition
{
    public LevelQuestion[] questions;
}

[Serializable]
public class LevelQuestion
{
    public int questionNumber;
    public string text;
    public string expectedAnswer;
    public int weight;
}

