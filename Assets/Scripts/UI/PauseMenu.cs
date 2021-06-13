using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button quitButton;
    private CanvasGroup canvasGroup;
    private bool isPaused = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        resumeButton.onClick.AddListener(() => { Resume(); });
        saveButton.onClick.AddListener(() => {   SaveButton(); });
        quitButton.onClick.AddListener(() => {   QuitButton(); });
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Pause()
    {
        GameMaster.instance.MouseUnlock();
        Time.timeScale = 0f;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        GameMaster.instance.MouseLock();
    }
    public void SaveButton()
    {
        StartManager.instance.SaveGame();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
