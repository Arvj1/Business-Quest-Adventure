using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class C3S1Manager : MonoBehaviour
{
    [SerializeField] List<Chap3_Stage1_SO> questionsData;
    [SerializeField] TMP_Text questionTxt;
    [SerializeField] CanvasGroup feedbackCG;
    [SerializeField] Image feedbackBG;
    [SerializeField] TMP_Text feedbackTxt;
    [SerializeField] GameObject rocketObj, truePlanet, falsePlanet;
    [SerializeField] Transform truePlanetTr, FalsePlanetTr;
    [SerializeField] Animator blastAnimator;
    [SerializeField] GameObject blocker, endPanel;

    private Chap3_Stage1_SO currentQuestionData;
    private int currIdx = 0;
    private Vector3 rocketInitialPosition;

    private void Start()
    {
        rocketInitialPosition = rocketObj.transform.position;
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        if(currIdx >=  questionsData.Count)
        {
            Debug.Log("No Questions to display");
            endPanel.SetActive(true);
            return;
        }

        currentQuestionData = questionsData[currIdx];
        questionTxt.text = currentQuestionData.Question;
    }


    public void CheckAnswer(bool _answer)
    {
        blocker.SetActive(true);
        if(_answer == currentQuestionData.answer)
        {
            feedbackBG.color = Color.green;
            feedbackTxt.text = "Correct Answer";
            StartCoroutine(AnswerAnimations(truePlanetTr, truePlanet, true));
        }
        else
        {
            feedbackBG.color = Color.red;
            feedbackTxt.text = "Wrong Answer";
            StartCoroutine(AnswerAnimations(FalsePlanetTr, falsePlanet, false));
        }
    }

    private IEnumerator AnswerAnimations(Transform endTr, GameObject answerPlanet, bool _answer)
    {
        rocketObj.transform.DOMove(endTr.position, 1f);

        // Calculate the angle to rotate the rocket to face the target
        Vector3 directionToTarget = (endTr.position - rocketObj.transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Rotate the rocket to face the target over 1 second
        if (currentQuestionData.answer)
            rocketObj.transform.DOLocalRotate(new Vector3(0, 0, -angle), 1f, RotateMode.Fast);
        else
            rocketObj.transform.DOLocalRotate(new Vector3(0, 0, angle), 1f, RotateMode.Fast);

        // Wait for the animation to complete
        yield return new WaitForSeconds(1f);

        if (_answer)
        {
            answerPlanet.gameObject.SetActive(false);
            Debug.Log(endTr.gameObject.name);
            blastAnimator.gameObject.transform.position = endTr.transform.position;
        }
        else
        {
            rocketObj.SetActive(false);
            blastAnimator.gameObject.transform.position = rocketObj.transform.position;
        }

        blastAnimator.gameObject.SetActive(true);
        blastAnimator.Play("Blast Animation");

        feedbackCG.gameObject.SetActive(true);
        feedbackCG.DOFade(1, 1f).OnComplete(() =>
        {
            feedbackCG.DOFade(0f, 1f).OnComplete(() =>
            {
                feedbackCG.gameObject.SetActive(false);

                rocketObj.transform.position = rocketInitialPosition;
                rocketObj.transform.rotation = Quaternion.identity;

                rocketObj.SetActive(true);
                answerPlanet.gameObject.SetActive(true);

                blastAnimator.gameObject.SetActive(false);
            });
        });

        blocker.SetActive(false);
        currIdx++;
        DisplayQuestion();
    }

}
