using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class C6S1GameManager : MonoBehaviour
{
    public List<Stage2Question> questionsData;
    public TMP_Text questionTxt, numberOfQuestionTxt;
    public List<TMP_Text> optionsTxt;
    public CanvasGroup correctAnswerFeedback, incorrectAnswerFeedback;
    public TMP_Text correctTxt, incorrectTxt;
    public UnityEvent OnGameOver;
    public bool pauseGame = false;

    public TMP_Text totalQuesTxt, correctAnswersTxt, incorrectAnsTxt;
    public GameObject explanationPanelPrefab;
    public Transform explantionPanelContentTr;

    private int currIdx = 0, correctCnt = 0;
    private List<WrongAnswerClass> wrongAnswersData = new List<WrongAnswerClass>();

    private void Start()
    {
        DisplayNextQuestion();
    }


    public void DisplayNextQuestion()
    {
        if(currIdx >= questionsData.Count)
        {
            Debug.Log("Game Over");
            pauseGame = true;

            totalQuesTxt.text = $"Total Questions : {questionsData.Count}";
            correctAnswersTxt.text = $"Correct : {correctCnt}";
            incorrectAnsTxt.text = $"Incorrect : {questionsData.Count - correctCnt}";

            for (int i = 0; i < wrongAnswersData.Count; i++)
            {
                GameObject explanationPanel = Instantiate(explanationPanelPrefab, explantionPanelContentTr);

                Transform tempVerPanel = explanationPanel.transform.GetChild(0);

                TMP_Text quesTxt = tempVerPanel.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                TMP_Text correctAnswerTxt = tempVerPanel.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                TMP_Text incorrectAnswerTxt = tempVerPanel.GetChild(1).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
                TMP_Text explanationTxt = tempVerPanel.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

                quesTxt.text = wrongAnswersData[i].questionData.questionText;
                correctAnswerTxt.text = wrongAnswersData[i].questionData.options[wrongAnswersData[i].questionData.correctOptionIndex];
                incorrectAnswerTxt.text = wrongAnswersData[i].incorrectAnswer;
                explanationTxt.text = wrongAnswersData[i].questionData.explanation;
            }

            OnGameOver.Invoke();
            return;
        }

        numberOfQuestionTxt.text = $"{currIdx+1}/{questionsData.Count}";

        Stage2Question question = questionsData[currIdx];
        questionTxt.text = question.questionText;
        for(int i=0; i<4; i++)
        {
            optionsTxt[i].text = question.options[i];
        }
    }

    public void CheckCorrectAnswer(string hitTxt)
    {
        if(currIdx >= questionsData.Count)
        {
            return;
        }

        pauseGame = true;
        if(hitTxt == questionsData[currIdx].options[questionsData[currIdx].correctOptionIndex])
        {
            Debug.Log("Correct Answer");
            correctTxt.text = FeedbackMessages.GetAppreciationMessage();
            correctCnt++;
            FadeInAndOut(correctAnswerFeedback);
        }
        else
        {
            WrongAnswerClass wad = new WrongAnswerClass();
            wad.questionData = questionsData[currIdx];
            wad.incorrectAnswer = hitTxt;
            wrongAnswersData.Add(wad);
            Debug.Log("Incorrect Answer");
            incorrectTxt.text = FeedbackMessages.GetGentleCorrectionMessage();
            FadeInAndOut(incorrectAnswerFeedback);
        }
    }

    public void FadeInAndOut(CanvasGroup cg)
    {
        cg.DOFade(1, 1f) // Fade in over 1 sec
            .OnComplete(() =>
            {
                cg.DOFade(0, 1f).OnComplete(() => {
                    currIdx++;
                    DisplayNextQuestion();
                    pauseGame = false;
                });
            });
    }
}
