using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WrongAnswerClass
{
    public Stage2Question questionData;
    public string incorrectAnswer;
}

public class Stage2Manager : MonoBehaviour
{
    [SerializeField] Stage2Question[] questionsData;
    [SerializeField] TMP_Text questionTxt, correctAnswerCountTxt, incorrectAnswerCountTxt;
    [SerializeField] TMP_Text[] options;
    [SerializeField] GameObject blocker;

    [SerializeField] Animator correctAnimator, incorrectAnimator;
    [SerializeField] Transform correctCharacterTr, incorrectCharacterTr;
    [SerializeField] Transform correctCharacterStartPos, correctCharacterEndPos, incorrectCharacterStartPos, incorrectCharacterEndPos;
    public UnityEvent OnGameOver;

    [SerializeField] TMP_Text resTotQues, resCorrectAns, resIncorrectAns;
    [SerializeField] GameObject explanationPanelPrefab;
    [SerializeField] Transform explanationScreenContentTr;
    [SerializeField] CanvasGroup correctCG, wrongCG;

    private List<WrongAnswerClass> wrongAnswersData = new List<WrongAnswerClass>();

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
        blocker.SetActive(true);
        StartCoroutine(CheckCorrectAnswerEnumerator(optionValue));
    }

    private IEnumerator CheckCorrectAnswerEnumerator(TMP_Text optionValue)
    {
        if (optionValue.text == currentQuestionData.options[currentQuestionData.correctOptionIndex])
        {
            yield return StartCoroutine(CorrectAnswerTasks());
        }
        else
        {
            WrongAnswerClass wAD = new WrongAnswerClass();
            wAD.questionData = currentQuestionData;
            wAD.incorrectAnswer = optionValue.text;
            wrongAnswersData.Add(wAD);

            yield return StartCoroutine(IncorrectAnswerTasks());
        }

        idx++;
        if (idx == questionsData.Length)
        {
            resTotQues.text = "Total Questions : " + questionsData.Length;
            resCorrectAns.text = "Correct : " + cAC;
            resIncorrectAns.text = "Incorrect : " + iAC;

            SetupExplanationScreen();

            OnGameOver.Invoke();
        }
        else ShowQuestion();
        
        blocker.SetActive(false);
    }

    private IEnumerator CorrectAnswerTasks()
    {
        cAC++;
        correctAnimator.Play("Run");
        incorrectAnimator.Play("IRun");

        correctCharacterTr.DOMove(correctCharacterEndPos.position, 1f).OnComplete(() =>
        {
            correctAnimator.Play("Attack");
        });
        incorrectCharacterTr.DOMove(incorrectCharacterEndPos.position, 1f).OnComplete(() =>
        {
            incorrectAnimator.Play("ITakeHit");
        });

        yield return new WaitForSeconds(1.5f);
        incorrectAnimator.Play("IDeath");
        correctCG.DOFade(1, 1f).OnComplete(() => { correctCG.DOFade(0, 1f); });
        yield return new WaitForSeconds(2f);

        correctCharacterTr.position = correctCharacterStartPos.position;
        incorrectCharacterTr.position = incorrectCharacterStartPos.position;

        correctAnimator.Play("Idle");
        incorrectAnimator.Play("IIdle");

        correctAnswerCountTxt.text = $"Player : {cAC.ToString()}";
    }

    private IEnumerator IncorrectAnswerTasks()
    {
        iAC++;

        correctAnimator.Play("Run");
        incorrectAnimator.Play("IRun");

        correctCharacterTr.DOMove(correctCharacterEndPos.position, 1f).OnComplete(() =>
        {
            correctAnimator.Play("TakeHit");
        });
        incorrectCharacterTr.DOMove(incorrectCharacterEndPos.position, 1f).OnComplete(() =>
        {
            incorrectAnimator.Play("IAttack");
        });

        yield return new WaitForSeconds(1.5f);
        correctAnimator.Play("Death");
        wrongCG.DOFade(1, 1f).OnComplete(() => { wrongCG.DOFade(0, 1f); });
        yield return new WaitForSeconds(2f);

        correctCharacterTr.position = correctCharacterStartPos.position;
        incorrectCharacterTr.position = incorrectCharacterStartPos.position;

        correctAnimator.Play("Idle");
        incorrectAnimator.Play("IIdle");

        incorrectAnswerCountTxt.text = $"Computer : {iAC.ToString()}";
    }

    private void SetupExplanationScreen()
    {
        for(int i=0; i<wrongAnswersData.Count; i++)
        {
            GameObject explanationPanel = Instantiate(explanationPanelPrefab, explanationScreenContentTr);

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
    }
}
