[System.Serializable]
public class Question {
    public int questionNumber;
    public string text;
    public string expectedAnswer;
    public int weight;

    // public static Question CreateFromJSON(string jsonString) {
    //     return JsonUtility.FromJson<Question>(jsonString);
    // }
}