using System;
using UnityEngine;

public class QuestManager : Singletone<QuestManager>
{
    [Header("Quset Card - NPC")]
    [SerializeField] private QuestCard_NPC questCardNpcPrefab;
    [SerializeField] private Transform npcQuestContainer;

    [Header("Quset Card - Player")]
    [SerializeField] private QuestCard_Player questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;

    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    private void Start()
    {
        LoadQuestsInNPCPanel();
    }

    private void LoadQuestsInNPCPanel()
    {
        for (int i = 0; i< quests.Length; i++)
        {
            if (quests[i].QuestAccepted == true)
            {
                if (quests[i].QuestCompleted == true) return;

                AcceptQuest(quests[i]);
                return;
            }

            QuestCard npcCard = Instantiate(questCardNpcPrefab, npcQuestContainer);
            npcCard.SetQuestUI(quests[i]);
        }
    }



    public void AcceptQuest(Quest quest)
    {
        QuestCard_Player playerCard = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        playerCard.SetQuestUI(quest);
    }


    // Äù½ºÆ® ÁøÃ´µµ °ü·Ã
    public void AddProgress(string questId,int amount)
    {
        Quest quest = QuestExists(questId);
        if (quest == null) return;

        if (quest.QuestAccepted == true)
        {
            quest.AddProgress(amount);
        }
    }




    private Quest QuestExists(string questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
                return quest;
        }

        return null;
    }
}
