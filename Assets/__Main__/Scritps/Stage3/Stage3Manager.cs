using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Stage3Items
{
    public string itemName;
    public int costPrice;
    public int sellPrice;
    public TMP_Text sellCountTxt;
    public int sellCount { get; set; } = 0;
}

public class Stage3Manager : MonoBehaviour
{
    [SerializeField] DialogueSO initialDiags;
    [SerializeField] List<Sprite> characterSprites;
    [SerializeField] Image customerImg;
    [SerializeField] RectTransform startRect, orderRect, endRect, customerRect;
    [SerializeField] TMP_Text customerReqTxt, totAmtTxt, ProfitTxt;
    [SerializeField] List<Stage3Items> itemsName;
    [SerializeField] CanvasGroup correctAnswer, incorrectAnswer, notEnoughMoney, doNotHaveItemStock;
    [SerializeField] GameObject completeObj;

    private int currentIdx = 0;
    private int orderedItemIdx = 0;
    private int totAmt = 1000, profit = 0;
    private int currCustomerCnt = 0;
    private bool canGive = false;
    private bool given = false;

    public void StartStage()
    {
        totAmtTxt.text = "Total Amount : " + totAmt;
        ProfitTxt.text = "Profit : " + profit;
    }

    private void OnEnable()
    {
        DialogueEvents.OnDialoguesEnd += StartGame;
    }

    private void StartGame()
    {
        canGive = false;
        given = false;
        if(currCustomerCnt == 4)
        {
            EndGame();
            return;
        }

        currCustomerCnt++;

        customerImg.sprite = characterSprites[UnityEngine.Random.Range(0, characterSprites.Count)];
        
        customerRect.anchoredPosition = startRect.anchoredPosition;
        customerRect.DOAnchorPos(orderRect.anchoredPosition, 2f).SetEase(Ease.InOutQuad).OnComplete(OnReachedOrderLoc);
    }

    private void OnReachedOrderLoc()
    {
        orderedItemIdx = UnityEngine.Random.Range(0, itemsName.Count);
        customerReqTxt.text = "I Would like to order " + itemsName[orderedItemIdx].itemName.ToUpper();
        customerReqTxt.transform.parent.gameObject.SetActive(true);
        canGive = true;
    }

    public void OnSeletedItem(string itemName)
    {
        if (!canGive || given) return;

        if(itemName.ToUpper() == itemsName[orderedItemIdx].itemName.ToUpper())
        {
            if (itemsName[orderedItemIdx].sellCount == 0)
            {
                FadeImage(doNotHaveItemStock);
                return;
            }

            given = true;
            FadeImage(correctAnswer);

            customerReqTxt.text = "Thank You!";

            profit += (itemsName[orderedItemIdx].sellPrice - itemsName[orderedItemIdx].costPrice);
            totAmt += itemsName[orderedItemIdx].sellPrice;

            itemsName[orderedItemIdx].sellCount--;
            itemsName[orderedItemIdx].sellCountTxt.text = itemsName[orderedItemIdx].sellCount.ToString();

            totAmtTxt.text = "Total Amount : " + totAmt;
            ProfitTxt.text = "Profit : " + profit;

            StartCoroutine(CleanupTask());
        }
        else
        {
            FadeImage(incorrectAnswer);
        }
    }

    IEnumerator CleanupTask()
    {
        yield return new WaitForSeconds(1f);

        customerReqTxt.transform.parent.gameObject.SetActive(false);
        customerRect.DOAnchorPos(endRect.anchoredPosition, 2f).SetEase(Ease.InOutQuad).OnComplete(OnReachedEnd);
    }

    private void OnReachedEnd()
    {
        StartGame();
    }

    private void FadeImage(CanvasGroup canvasGroup)
    {
        // Set initial alpha to 0 (fully transparent)
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false; // Make it non-interactable

        // Fade in
        canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            canvasGroup.interactable = true; // Make it interactable after fade-in

            // Wait for 1 second
            DOVirtual.DelayedCall(1f, () =>
            {
                // Fade out
                canvasGroup.DOFade(0, 1f).OnComplete(() =>
                {
                    canvasGroup.interactable = false; // Make it non-interactable after fade-out
                });
            });
        });
    }

    public void GotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Buy(string itemName)
    {
        foreach(var item in itemsName)
        {
            if (item.itemName.ToUpper() == itemName.ToUpper())
            {
                if (totAmt >= item.costPrice)
                { 
                    item.sellCount++;
                    item.sellCountTxt.text = item.sellCount.ToString();

                    totAmt -= item.costPrice;
                    totAmtTxt.text = "Total Amount : " + totAmt;
                }
                else
                {
                    FadeImage(notEnoughMoney);
                }
            }
        }
    }

    public void EndGame()
    {
        completeObj.SetActive(true);
    }
}
