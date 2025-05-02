using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCard_Player : QuestCard
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI rewardGoldValueText;
    [SerializeField] private TextMeshProUGUI rewardExpValueText;
    [SerializeField] private TextMeshProUGUI rewardItemQuantityText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Image rewardItem_Icon;

    [Header("Complete Quest")]
    [SerializeField] private GameObject questCompleteButton;


    private void Update()
    {
        UpdateProcessStatus(TargetQuest);
    }

    public override void SetQuestUI(Quest quest)
    {
        base.SetQuestUI(quest);

        rewardGoldValueText.text = quest.GoldReward.ToString();
        rewardExpValueText.text = quest.ExpReward.ToString();
        rewardItemQuantityText.text = quest.ItemReward.Quantity.ToString();
        rewardItem_Icon.sprite = quest.ItemReward.Item.Icon;
        UpdateProcessStatus(quest);
    }

    private void UpdateProcessStatus(Quest quest)
    {
        statusText.text = $"Status\n{quest.CurrentStatus}/{quest.QuestGoal}";
        QuestCompletedCheck();
    }

    // 퀘스트 완료
    public void CompleteQuest()
    {
        // Exp 보상 부여
        GameManager.Instance.AddPlayerExp(TargetQuest.ExpReward);
        // 아이템 보상 부여
        Inventory.Instance.AddItem
            (TargetQuest.ItemReward.Item, TargetQuest.ItemReward.Quantity);
        // 골드 보상 부여
        GoldManager.Instance.AddGolds(TargetQuest.GoldReward);

        gameObject.SetActive(false);
    }


    private void QuestCompletedCheck()
    {
        if (TargetQuest.QuestCompleted == true)
        {
            questCompleteButton.SetActive(true);
        }
    }

}
