using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [Header("Quest info")]
    public string Name;
    public string ID;
    public int QuestGoal;

    [Header("Description")]
    [TextArea] public string Descritpion;

    [Header("Reward")]
    public int GoldReward;
    public float ExpReward;
    public Quest_ItemReward ItemReward;

    [Header("Status")]
    public int CurrentStatus;
    public bool QuestCompleted;
    public bool QuestAccepted;


    public void AddProgress(int amount)
    {
        CurrentStatus += amount;

        if (CurrentStatus >= QuestGoal)
        {
            CurrentStatus = QuestGoal;
            QuestComplete();
        }
    }

    private void QuestComplete()
    {
        if (QuestCompleted) return;

        QuestCompleted = true;
    }

    public void ResetQuest()
    {
        QuestAccepted = false;
        QuestCompleted = false;
        CurrentStatus = 0;
    }


}

[Serializable]
public class Quest_ItemReward
{
    public Item_Base Item;
    public int Quantity;
}
