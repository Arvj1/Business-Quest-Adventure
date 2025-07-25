using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CH : MonoBehaviour
{
    [SerializeField] Transform dragStPos, dragEndPos;
    [SerializeField] Transform dropStPos, dropStopPos, dropEndPos;
    [SerializeField] List<GameObject> dropItems;
    [SerializeField] GameObject dragParent;
    [SerializeField] CanvasGroup correctCG, wrongCG;
    [SerializeField] TMP_Text correctTxt, incorrectTxt;
    public UnityEvent OnGameOver;

    private int idx = 0;

    private void Start()
    {
        dragParent.transform.DOMove(dragEndPos.position, 1f).OnComplete(() =>
        {
            MoveDropLocation();
        });
    }

    private void MoveDropLocation()
    {
        if (idx == dropItems.Count)
        {
            OnGameOver.Invoke();
            return;
        }
        dropItems[idx].transform.DOMove(dropStopPos.position, 1f);
    }

    public void OnDropped(GameObject dragged, GameObject dropped)
    {
        if (dragged.CompareTag(dropped.tag))
        {
            correctTxt.text = FeedbackMessages.GetAppreciationMessage();
            correctCG.DOFade(1, 3f).OnComplete(() =>
            {
                correctCG.DOFade(0, 3f);
            });
            dropItems[idx].transform.DOMove(dropEndPos.position, 1f).OnComplete(() =>
            {
                idx++;
                MoveDropLocation();
            });
        }
        else
        {
            incorrectTxt.text = FeedbackMessages.GetGentleCorrectionMessage();
            wrongCG.DOFade(1, 3).OnComplete(() =>
            {
                wrongCG.DOFade(0, 3f);
            });
        }
    }
}
