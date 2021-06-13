using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static StartManager instance;
    public delegate void OnSaveSystem();
    public OnSaveSystem OnSaveSystemCallBack;
    public delegate void OnLoadSystem();
    public OnSaveSystem OnLoadSystemCallBack;
    public SavedData savedData;
    public Texture2D mouseImage;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one StartManager Instance found");
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(mouseImage, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }
    public void SaveGame()
    {
        savedData = new SavedData();
        OnSaveSystemCallBack.Invoke();
        SaveLoadSystem.Save(savedData, "SaveGame");
    }
    public void LoadGame()
    {
        savedData = new SavedData();
        savedData = SaveLoadSystem.Load("SaveGame");
        OnLoadSystemCallBack?.Invoke();
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Roan Town");
    }
    public IEnumerator Continue()
    {
        SceneManager.LoadScene("Roan Town");
        while (SceneManager.GetActiveScene().buildIndex != 1)
        {
            yield return null;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadGame();
        }
    }
    public void Wrapper()
    {
        StartCoroutine(Continue());
    }
    public void Quit()
    {
        Application.Quit();
    }
}
