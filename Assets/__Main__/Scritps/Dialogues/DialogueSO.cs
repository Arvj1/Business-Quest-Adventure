using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "Game/Dialogues", order = 1)]
public class DialogueSO : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    [TextArea]
    public List<string> dialogues = new List<string>();

    public bool shouldRemainActiveAtEnd = false;
}
