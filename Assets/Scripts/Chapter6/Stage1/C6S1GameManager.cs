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
    public UnityEvent OnGameOver;
    public bool pauseGame = false;

    private int currIdx = 0, correctCnt = 0;

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
        pauseGame = true;
        if(hitTxt == questionsData[currIdx].options[questionsData[currIdx].correctOptionIndex])
        {
            Debug.Log("Correct Answer");
            correctCnt++;
            FadeInAndOut(correctAnswerFeedback);
        }
        else
        {
            Debug.Log("Incorrect Answer");
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
