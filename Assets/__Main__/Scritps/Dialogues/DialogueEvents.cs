using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class DialogueEvents 
{
    public static UnityAction<DialogueSO> StartDialogues = delegate { };
    public static UnityAction StopDialogues = delegate { };
    public static UnityAction OnDialoguesEnd = delegate { };
}
