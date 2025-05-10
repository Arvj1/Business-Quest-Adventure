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

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 2; i <= 15; i++)
        {
            int chapterNumber = i; // Capture the loop variable

            GameObject chapterListItemObj = Instantiate(chapterListItem, parentTr);
            Button btn = chapterListItemObj.GetComponent<Button>();

            btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene($"CH{chapterNumber}_Instructions");
            });

            btn.interactable = PersistingMenuScript.Instance.levelData.unlockedLevels[i];

            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Chapter {chapterNumber}";
        }
    }
}
