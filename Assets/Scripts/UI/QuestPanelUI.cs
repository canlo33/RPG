using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelUI : MonoBehaviour
{
    public GameObject firstGoal;
    public GameObject secondGoal;
    public TextMeshProUGUI npcName;
    public Image npcPicture;
    public TextMeshProUGUI npcText;
    public TextMeshProUGUI firstGoalText;
    public TextMeshProUGUI secondGoalText;
    public Image firstGoalStatus;
    public Image secondGoalStatus;
    public TextMeshProUGUI goldRewardText;
    public TextMeshProUGUI expRewardText;
    public GameObject goldReward;
    public GameObject expReward;
    public GameObject itemReward;
    public TextMeshProUGUI itemRewardText;
    public TextMeshProUGUI itemAmountText;
    public Image itemIcon;
    public GameObject acceptButton;
    public GameObject rejectButton;
    public GameObject submitButton;
    public GameObject abondonButton;
    public Texture2D mouseImage;
    public Sprite greenTick;
    public Sprite redCross;
    [SerializeField] private AudioClip congratsAudio;
    [SerializeField] private AudioClip buttonClickAudio;
    [SerializeField]private Quest Quest { get; set; }
    private QuestGiver questGiver;
    private CanvasGroup canvasGroup;
    private GameMaster gameMaster;

    private void Start()
    {
        gameMaster = GameMaster.instance;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        UIManager.instance.onUITriggeredCallBack += CloseButton;
        CloseButton();        
    }
    public void DisplayUI(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
        Quest = questGiver.Quest;
        npcPicture.sprite = questGiver.npcIcon;
        npcName.text = "" + questGiver.name;
        acceptButton.SetActive(false);
        rejectButton.SetActive(false);
        submitButton.SetActive(false);
        abondonButton.SetActive(false);
        itemReward.SetActive(false);
        goldReward.SetActive(false);
        expReward.SetActive(false);
        secondGoal.SetActive(false);
        firstGoal.SetActive(false);
        firstGoalStatus.enabled = false;
        secondGoalStatus.enabled = false;
        questGiver.CheckQuest();
        gameMaster.MouseUnlock();
        if (InventoryManager.instance.onInventoryChangedCallBack != null)
            InventoryManager.instance.onInventoryChangedCallBack.Invoke();
        //Check if the quest is already completed or not.
        if (!questGiver.QuestCompleted)
        {
            //Quest is not assigned yet. Open the Quest Info Screen for the player.
            if (!questGiver.QuestAssigned)
            {
                gameObject.SetActive(true);
                firstGoal.SetActive(true);
                if (Quest.Goals.Count == 2)
                {
                    secondGoal.SetActive(true);
                    secondGoalText.text = Quest.Goals[1].Description;
                }
                goldReward.SetActive(true);
                expReward.SetActive(true);
                goldRewardText.text = Quest.GoldReward + " Gold ";
                expRewardText.text = Quest.ExpReward + " Exp";
                if (Quest.ItemReward != null)
                {
                    itemReward.SetActive(true);
                    itemIcon.sprite = Quest.ItemReward.icon;
                    itemRewardText.text = Quest.ItemReward.name;
                    itemAmountText.text = "" + Quest.ItemAmount;
                }
                firstGoalText.text = Quest.Goals[0].Description;
                npcText.text = questGiver.Sentences[1];
                acceptButton.SetActive(true);
                rejectButton.SetActive(true);
            }
            // Check if the player finished the quest or not?
            else if (questGiver.QuestAssigned)
            {
                //questGiver.CheckQuest();
                //Check if the quest is complete or not.
                if (Quest.GoalsCompleted)
                {
                    gameObject.SetActive(true);
                    firstGoal.SetActive(true);
                    if (Quest.Goals.Count == 2)
                    {
                        secondGoal.SetActive(true);
                        secondGoalText.text = Quest.Goals[1].Description;
                    }
                    goldReward.SetActive(true);
                    expReward.SetActive(true);
                    goldRewardText.text = Quest.GoldReward + " Gold ";
                    expRewardText.text = Quest.ExpReward + " Exp";
                    if (Quest.ItemReward != null)
                    {
                        itemReward.SetActive(true);
                        itemIcon.sprite = Quest.ItemReward.icon;
                        itemRewardText.text = Quest.ItemReward.name;
                        itemAmountText.text = "" + Quest.ItemAmount;
                    }
                    firstGoalText.text = Quest.Goals[0].Description;
                    firstGoalStatus.enabled = true;
                    secondGoalStatus.enabled = true;
                    firstGoalStatus.sprite = greenTick;
                    secondGoalStatus.sprite = greenTick;
                    npcText.text = questGiver.Sentences[3];
                    submitButton.SetActive(true);
                    submitButton.GetComponent<Button>().onClick.AddListener(() => { SubmitButton();  });
                }
                else if (!Quest.GoalsCompleted)
                {
                    gameObject.SetActive(true);
                    firstGoal.SetActive(true);
                    goldReward.SetActive(true);
                    expReward.SetActive(true);
                    goldRewardText.text = Quest.GoldReward + " Gold ";
                    expRewardText.text = Quest.ExpReward + " Exp";
                    if (Quest.ItemReward != null)
                    {
                        itemReward.SetActive(true);
                        itemIcon.sprite = Quest.ItemReward.icon;
                        itemRewardText.text = Quest.ItemReward.name;
                        itemAmountText.text = "" + Quest.ItemAmount;
                    }
                    firstGoalText.text = Quest.Goals[0].Description;
                    firstGoalStatus.enabled = true;
                    if (Quest.Goals[0].Completed)
                        firstGoalStatus.sprite = greenTick;
                    else
                        firstGoalStatus.sprite = redCross;
                    if (Quest.Goals.Count == 2 && Quest.Goals[1].Completed)
                    {
                        secondGoal.SetActive(true);
                        secondGoalStatus.sprite = greenTick;
                        secondGoalStatus.enabled = true;
                        secondGoalText.text = Quest.Goals[1].Description;
                    }
                    else if (Quest.Goals.Count == 2 && !Quest.Goals[1].Completed)
                    {
                        secondGoal.SetActive(true);
                        secondGoalStatus.sprite = redCross;
                        secondGoalStatus.enabled = true;
                        secondGoalText.text = Quest.Goals[1].Description;
                    }
                    npcText.text = questGiver.Sentences[2];
                    abondonButton.SetActive(true);
                }
            }
        }
        else if (questGiver.QuestCompleted)
        {
            gameObject.SetActive(true);
            npcText.text = questGiver.Sentences[4];
        }
    }
    public void CloseButton()
    {
        gameObject.SetActive(false);
        gameMaster.MouseLock();        
    }
    public void AssignQuest()
    {
        questGiver.AssignQuest();
        questGiver.PlaySoundEffect(buttonClickAudio); 
        CloseButton();
    }
    public void SubmitButton()
    {
       if(!Quest.Completed)
        {
            Quest.GiveReward();
            questGiver.PlaySoundEffect(congratsAudio);
        }          
       CloseButton();
    }
    public void AbandonButton()
    {
        questGiver.AbandonQuest();
        CloseButton();
    }


}
