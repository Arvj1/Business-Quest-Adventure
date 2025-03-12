using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DialogueSO dialoguesData;
    [SerializeField] TMP_Text nameTxt, dialogueTxt;
    [SerializeField] Image characterImg;
    [SerializeField] Image backgroundImg;

    private int currIdx = 0, IdxN;


    private void OnEnable()
    {
        DialogueEvents.StartDialogues += Show;
        DialogueEvents.StopDialogues += Stop;
    }

    private void OnDisable()
    {
        DialogueEvents.StartDialogues -= Show;
        DialogueEvents.StopDialogues -= Stop;
    }

    public void Show(DialogueSO dialogues)
    {
        Invoke("MakeClicksActive", 1f);
        nameTxt.transform.parent.gameObject.SetActive(true);

        currIdx = 0;

        this.dialoguesData = dialogues;

        IdxN = dialoguesData.dialogues.Count;
        characterImg.sprite = dialoguesData.characterSprite;
        nameTxt.text = dialoguesData.name;

        NextDialogue();
    }

    public void MakeClicksActive()
    {
        backgroundImg.gameObject.SetActive(true);
    }

    public void NextDialogue()
    {
        if(currIdx >= IdxN)
        {
            if (dialoguesData.shouldRemainActiveAtEnd)
                backgroundImg.raycastTarget = false;
            else
            {
                Stop();
            }

            return;
        }

        dialogueTxt.text = dialoguesData.dialogues[currIdx];
        currIdx++;
    }

    public void Stop()
    {
        currIdx = 0;
        backgroundImg.raycastTarget = true;

        backgroundImg.gameObject.SetActive(false);
        nameTxt.transform.parent.gameObject.SetActive(false);

        DialogueEvents.OnDialoguesEnd.Invoke();
        DialogueEvents.OnDialoguesEnd = delegate { };
    }
}
