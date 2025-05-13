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
    [SerializeField] Button playButton;

    private void Start()
    {
        Setup();
    }

    public void OnAudioSliderValueChange(float value)
    {
        PersistingMenuScript.Instance.bgAudioSource.volume = value;
        percentageTxt.text = (value * 100).ToString("F0") + "%";
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

        for(int i=15; i>=2; i--)
        {
            if (PersistingMenuScript.Instance.levelData.unlockedLevels[i])
            {
                playButton.onClick.AddListener(() => { SceneManager.LoadScene($"CH{i}_Instruction"); });
                break;
            }
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

            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Chapter {chapterNumber}";
        }
    }
}
