using BayatGames.SaveGameFree;
using System;
using System.Collections.Generic;
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
    public Quest[] Quests { get; set; }
    private List<Quest> acceptedQuest = new List<Quest>();

    public NPCInteraction CurrentQuestNPC { get; set; }

    // 퀘스트 저장용 키
    private readonly string QUEST_KEY_DATA = "MY_QUESTS";


    public void SaveQuests()
    {
        QuestSaveData saveData = new QuestSaveData();
        saveData.QuestIDs = new string[acceptedQuest.Count];
        saveData.QuestAccepted = new bool[acceptedQuest.Count];
        saveData.QuestCompleted = new bool[acceptedQuest.Count];
        saveData.CurrentStatus = new int[acceptedQuest.Count];

        for (int i = 0; i < acceptedQuest.Count; i++)
        {
            saveData.QuestIDs[i] = acceptedQuest[i].ID;
            saveData.QuestAccepted[i] = acceptedQuest[i].QuestAccepted;
            saveData.QuestCompleted[i] = acceptedQuest[i].QuestCompleted;
            saveData.CurrentStatus[i] = acceptedQuest[i].CurrentStatus;
        }

        SaveGame.Save(QUEST_KEY_DATA, saveData);
    }

    public void LoadQuests()
    {
        // Quests가 null인지 체크
        if (Quests == null || Quests.Length == 0)
        {
            Debug.LogWarning("Quests 배열이 초기화되지 않았습니다. 퀘스트 로드를 건너뜁니다.");
            return;
        }

        if (SaveGame.Exists(QUEST_KEY_DATA))
        {
            QuestSaveData loadData = SaveGame.Load<QuestSaveData>(QUEST_KEY_DATA);

            if (loadData.QuestIDs == null || loadData.QuestIDs.Length == 0)
            {
                Debug.Log("저장된 퀘스트 데이터가 없습니다.");
                return;
            }

            for (int i = 0; i < loadData.QuestIDs.Length; i++)
            {
                Quest quest = FindQuestByID(loadData.QuestIDs[i]);
                if (quest != null)
                {
                    quest.QuestAccepted = loadData.QuestAccepted[i];
                    quest.QuestCompleted = loadData.QuestCompleted[i];
                    quest.CurrentStatus = loadData.CurrentStatus[i];

                    if (quest.QuestAccepted && !quest.QuestCompleted)
                    {
                        AcceptQuest(quest);
                    }
                }
            }
        }
    }

    private Quest FindQuestByID(string questID)
    {
        // Quests가 null인지 체크
        if (Quests == null || Quests.Length == 0)
        {
            Debug.LogWarning("Quests 배열이 초기화되지 않았습니다.");
            return null;
        }

        foreach (Quest quest in Quests)
        {
            if (quest != null && quest.ID == questID)
                return quest;
        }
        return null;
    }

    public void LoadQuestsInNPCPanel()
    {
        ClearNPCQuestPanel();

        for (int i = 0; i< Quests.Length; i++)
        {
            if (Quests[i].QuestAccepted == true)
            {
                if (Quests[i].QuestCompleted == true) return;

                AcceptQuest(Quests[i]);
                return;
            }

            QuestCard npcCard = Instantiate(questCardNpcPrefab, npcQuestContainer);
            npcCard.SetQuestUI(Quests[i]);
        }
    }



    public void ClearNPCQuestPanel()
    {
        if (npcQuestContainer == null)  return;

        int childCount = npcQuestContainer.childCount;

        if (childCount <= 0) return;

        // 생성되어 있는 퀘스트카드 모두 삭제
        for (int i = 0; i < childCount; i++ )
        {
            Transform child = npcQuestContainer.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void AcceptQuest(Quest quest)
    {
        QuestCard_Player playerCard = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        playerCard.SetQuestUI(quest);

        acceptedQuest.Add(quest);
    }


    // 퀘스트 진척도 관련
    public void AddProgress(string questId,int amount)
    {
        Quest quest = QuestExists(questId);
        if (quest == null) return;

        if (quest.QuestAccepted == true)
        {
            quest.AddProgress(amount);
        }
    }


    // 퀘스트 존재 여부 검사
    private Quest QuestExists(string questID)
    {
        if (acceptedQuest.Count <= 0)
            return null;

        foreach (Quest quest in acceptedQuest)
        {
            if (quest.ID == questID)
                return quest;
        }

        return null;
    }
}
