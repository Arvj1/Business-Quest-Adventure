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
    [SerializeField] TMP_Text endPanelTotQuesTxt, endPanelCorrectCountTxt, endPanelWrongCountTxt;
    [SerializeField] GameObject explanationPanelPrefab;
    [SerializeField] Transform explantionPanelContentTr;

    private List<Chap3_Stage1_SO> wrongAnswersData = new List<Chap3_Stage1_SO>();
    private Chap3_Stage1_SO currentQuestionData;
    private int currIdx = 0, correctCnt = 0;
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
            SetupEndPanel();
            endPanel.SetActive(true);
            return;
        }

        currentQuestionData = questionsData[currIdx];
        questionTxt.text = currentQuestionData.Question;
    }

    private void SetupEndPanel()
    {
        endPanelTotQuesTxt.text = $"Total Questions :- {questionsData.Count}"; 
        endPanelCorrectCountTxt.text = $"Correct :- {correctCnt}"; 
        endPanelWrongCountTxt.text = $"Total Questions :- {questionsData.Count - correctCnt}"; 

        foreach(Chap3_Stage1_SO qd in wrongAnswersData)
        {
            GameObject explanationObj = Instantiate(explanationPanelPrefab, explantionPanelContentTr);
            Transform tempVerPanel = explanationObj.transform.GetChild(0);

            TMP_Text quesTxt = tempVerPanel.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text correctAnswerTxt = tempVerPanel.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text incorrectAnswerTxt = tempVerPanel.GetChild(1).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text explanationTxt = tempVerPanel.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

            quesTxt.text = qd.Question;
            correctAnswerTxt.text = (qd.answer) ? "True" : "False";
            incorrectAnswerTxt.text = (!qd.answer) ? "True" : "False";
            explanationTxt.text = qd.explenation;
        }
    }


    public void CheckAnswer(bool _answer)
    {
        blocker.SetActive(true);

        bool selectedCorrect = true;
        if(_answer == currentQuestionData.answer)
        {
            correctCnt++;
            selectedCorrect = true;
            feedbackBG.color = Color.green;
            feedbackTxt.text = "Correct Answer";
        }
        else
        {
            selectedCorrect = false;
            feedbackBG.color = Color.red;
            feedbackTxt.text = "Wrong Answer";
            wrongAnswersData.Add(currentQuestionData);
        }

        if (_answer) StartCoroutine(AnswerAnimations(truePlanetTr, truePlanet, selectedCorrect));
        else StartCoroutine(AnswerAnimations(FalsePlanetTr, falsePlanet, selectedCorrect));
    }

    private IEnumerator AnswerAnimations(Transform endTr, GameObject answerPlanet, bool _answer)
    {
        rocketObj.transform.DOMove(endTr.position, 1f).SetEase(Ease.Linear);

        // Rotate the rocket to face the target continuously
        rocketObj.transform.DORotateQuaternion(
            Quaternion.LookRotation(Vector3.forward, endTr.position - rocketObj.transform.position),
            0.2f
        );

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
