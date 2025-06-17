using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject chapterListItem;
    [SerializeField] Transform parentTr;
    [SerializeField] TMP_Text percentageTxt, playBtnText;
    [SerializeField] Slider bgAudioSlider;

    private void Start()
    {
        Setup();
    }

    public void OnAudioSliderValueChange(float value)
    {
        PersistingMenuScript.Instance.bgAudioSource.volume = value;
        percentageTxt.text = (value * 100).ToString("F0") + "%";
    }

    public void PlayButton()
    {
        bool found = false;
        for (int i = 2; i < 15; i++)
        {
            if (!PersistingMenuScript.Instance.levelData.unlockedLevels[i + 1])
            {
                SceneManager.LoadScene($"CH{i}_Instruction");
                found = true;
                break;
            }
        }

        if (!found)
        {
            SceneManager.LoadScene($"CH{15}_Instruction");
        }
    }

    private void Setup()
    {
        if (PersistingMenuScript.Instance.levelData.unlockedLevels[3])
        {
            playBtnText.text = "Resume Game";
        }
        else
        {
            playBtnText.text = "Start New Game";
        }



        bgAudioSlider.value = PersistingMenuScript.Instance.bgAudioSource.volume;
        percentageTxt.text = (PersistingMenuScript.Instance.bgAudioSource.volume * 100f).ToString("F0") + "%";

        for (int i = 2; i <= 15; i++)
        {
            int chapterNumber = i; // Capture the loop variable

            GameObject chapterListItemObj = Instantiate(chapterListItem, parentTr);
            Button btn = chapterListItemObj.GetComponent<Button>();

            btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene($"CH{chapterNumber}_Instruction");
            });

            btn.interactable = PersistingMenuScript.Instance.levelData.unlockedLevels[i];
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Chapter {chapterNumber - 1}";
        }
    }
}
