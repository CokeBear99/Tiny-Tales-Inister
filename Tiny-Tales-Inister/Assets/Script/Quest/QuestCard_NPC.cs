using TMPro;
using UnityEngine;

public class QuestCard_NPC : QuestCard
{
    [SerializeField] private TextMeshProUGUI questRewardText;

    public override void SetQuestUI(Quest quest)
    {
        base.SetQuestUI(quest);

        questRewardText.text = $"- {quest.GoldReward} Gold\n" +
                               $"- {quest.ExpReward} Exp\n" +
                               $"- {quest.ItemReward.Item.Name} x{quest.ItemReward.Quantity}";
    }

    public void AcceptQuest()
    {
        if (TargetQuest == null) return;

        TargetQuest.QuestAccepted = true;
        QuestManager.Instance.AcceptQuest(TargetQuest);
        gameObject.SetActive(false);
    }


}
