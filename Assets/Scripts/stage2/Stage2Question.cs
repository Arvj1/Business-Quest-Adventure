using UnityEngine;

[CreateAssetMenu(fileName = "Stage2Question", menuName = "ScriptableObjects/Stage2Question", order = 1)]
public class Stage2Question : ScriptableObject
{
    [TextArea]
    public string questionText; // The question text

    public string[] options = new string[4]; // Array for 4 options

    [Range(0, 3)]
    public int correctOptionIndex; // Index of the correct option (0-3)
}
