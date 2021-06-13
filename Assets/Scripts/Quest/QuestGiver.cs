using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool QuestAssigned { get; set; }
    public bool QuestCompleted { get; set; }
    public Quest Quest { get; set; }
    private PlayerStats playerStats;
    private GameObject questHolder;
    [SerializeField]
    private string questType;
    private QuestPanelUI questPanel;
    private GameMaster gameMaster;
    private StartManager startManager;
    private AudioSource audioSource;
    public override void Start()
    {
        gameMaster = GameMaster.instance;
        startManager = StartManager.instance;
        audioSource = GetComponent<AudioSource>();
        base.Start();
        questHolder = GameObject.FindGameObjectWithTag("QuestHolder");
        playerStats = gameMaster.playerStats;
        questPanel = gameMaster.questPanel;
        Quest = (Quest)questHolder.AddComponent(System.Type.GetType(questType));
        startManager.OnSaveSystemCallBack += SaveQuest;
        startManager.OnLoadSystemCallBack += LoadQuest;
    }

    public override void Interact()
    {            
        questPanel.DisplayUI(this);
    }

    public void AssignQuest()
    {
        if (playerStats.level < Quest.RequiredLevel || QuestAssigned)
            return;
        Quest.Init();
        QuestAssigned = true;
    }
    public void CheckQuest()
    {
        if (Quest.Completed)
        {
            QuestCompleted = true;
        }
    }
    public void AbandonQuest()
    {
        QuestAssigned = false;
        Quest.Completed = false;
        Quest.UnInit();
    }
    private void SaveQuest()
    {
        startManager.savedData.npcNames.Add(name);
        startManager.savedData.isQuestComplete.Add(QuestCompleted);
        startManager.savedData.isQuestAssigned.Add(QuestAssigned);
    }
    private void LoadQuest()
    {
        
        for (int i = 0; i < startManager.savedData.npcNames.Count; i++)
        {
            if (name == startManager.savedData.npcNames[i])
            {
                QuestAssigned = startManager.savedData.isQuestAssigned[i];
                QuestCompleted = startManager.savedData.isQuestComplete[i];
                break;
            }                
        }
        if (QuestAssigned)
            Quest.Init();        
    }
    public void PlaySoundEffect(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
