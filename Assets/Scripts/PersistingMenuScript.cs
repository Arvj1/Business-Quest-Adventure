using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistingMenuScript : MonoBehaviour
{
    public static PersistingMenuScript Instance;
    public LevelsDataSO levelData;
    public AudioSource bgAudioSource;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button pauseButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            pauseButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePausePanel();
        }
    }

    public void EnablePausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisablePausePanel()
    {
        Time.timeScale = 1f;
    }
}
