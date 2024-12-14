using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Stage2Manager : MonoBehaviour
{
    [SerializeField] Stage2Question[] questionsData;
    [SerializeField] TMP_Text questionTxt, correctAnswerCountTxt, incorrectAnswerCountTxt;
    [SerializeField] TMP_Text[] options;

    [SerializeField] Animator correctAnimator, incorrectAnimator;
    [SerializeField] Transform correctCharacterTr, incorrectCharacterTr;
    [SerializeField] Transform correctCharacterStartPos, correctCharacterEndPos, incorrectCharacterStartPos, incorrectCharacterEndPos;
    public UnityEvent OnGameOver;

    private Stage2Question currentQuestionData;
    private int idx = 0, cAC = 0, iAC = 0;

    private void Start()
    {
        correctAnimator.Play("Idle");
        incorrectAnimator.Play("IIdle");
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        currentQuestionData = questionsData[idx];
        questionTxt.text = currentQuestionData.questionText;

        for(int i=0; i<options.Length; i++)
        {
            options[i].text = currentQuestionData.options[i];
        }
    }

    public void CheckCorrectAnswer(TMP_Text optionValue)
    {
        if(optionValue.text == currentQuestionData.options[currentQuestionData.correctOptionIndex])
        {
            StartCoroutine(CorrectAnswerTasks());
        }
        else
        {
            iAC++;
            incorrectAnswerCountTxt.text = iAC.ToString();
        }
        
        idx++;
        if(idx == questionsData.Length)
        {
            Debug.Log("Game Over");
            OnGameOver.Invoke();
        }
        else ShowQuestion();
    }

    private IEnumerator CorrectAnswerTasks()
    {
        cAC++;
        correctAnimator.Play("Run");
        correctCharacterTr.DOMove(correctCharacterEndPos.position, 1f).OnComplete(() =>
        {
            correctAnimator.Play("Attack");

        });
        incorrectCharacterTr.DOMove(incorrectCharacterEndPos.position, 1f).OnComplete(() =>
        {
            incorrectAnimator.Play("ITakeHit");
        });

        yield return new WaitForSeconds(0.5f);
        incorrectAnimator.Play("Death");
        yield return new WaitForSeconds(0.5f);


        correctAnswerCountTxt.text = cAC.ToString();
    }
}
