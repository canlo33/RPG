using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    #region Singleton
    public static GameMaster instance;
    public delegate void OnPlayerLevelUp();
    public OnPlayerLevelUp OnPlayerLevelUpCallBack;
    private void Awake()
    {
        if (instance != null)
        {            
            Debug.LogError("More than one GameMaster Instance found");
            Destroy(this);
            return;
        }            
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<MageController>();
        playerStats = playerController.playerStats;
        questPanel = GameObject.FindGameObjectWithTag("QuestPanel").GetComponent<QuestPanelUI>();
        StartManager.instance.OnSaveSystemCallBack += playerStats.SaveProgress;
        StartManager.instance.OnLoadSystemCallBack += playerStats.LoadProgress;
    }
    #endregion
    public GameObject mobInfo;
    public TextMeshProUGUI mobName;
    public Slider mobHPBar;
    public GameObject player;
    public MageController playerController;
    public CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;
    private float shakeDuration;
    private float startIntensity;
    public PlayerStats playerStats;
    public QuestPanelUI questPanel;
    public GameObject levelUpPanel;
    public GameObject playerDiedPanel;
    public GameObject toolTip;
    public Texture2D mouseImage;
    public bool isMousedLocked = true;
    private void Start()
    {
        mobInfo.SetActive(false);
        levelUpPanel.SetActive(false);
        playerDiedPanel.GetComponent<CanvasGroup>().alpha = 1;
        playerDiedPanel.GetComponent<CanvasGroup>().interactable = true;
        playerDiedPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        playerDiedPanel.SetActive(false);
        playerStats.Reset();
        cinemachinePerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        MouseLock();
    }
    void Update()
    {
        DisplayTargetUI();
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachinePerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTimer / shakeDuration));
        }
    }
    void DisplayTargetUI()
    {
        if (playerController.target != null)
        {
            mobInfo.SetActive(true);
            mobName.text = playerController.target.name;
            mobHPBar.value = playerController.target.GetComponent<Entity>().healthSystem.GetHealthPercentage();
        }
        else
            mobInfo.SetActive(false);
    }
    public void MouseUnlock()
    { 
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
        Cursor.SetCursor(mouseImage, Vector2.zero, CursorMode.Auto);
        playerController.aim.enabled = false;
        playerController.canCast = false;
        playerController.canMove = false;
        playerController.canRotate = false;
        playerController.aoeMarker.SetActive(false);
        isMousedLocked = false;
    }
    public void MouseLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.aim.enabled = true;
        playerController.canCast = true;
        playerController.canMove = true;
        playerController.canRotate = true;
        isMousedLocked = true;
    }
    public void CameraShake(float intensity, float duration)
    {
        if (shakeTimer > 0)
            return;
        shakeTimer = duration;
        shakeDuration = duration;
        cinemachinePerlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
    }
}
