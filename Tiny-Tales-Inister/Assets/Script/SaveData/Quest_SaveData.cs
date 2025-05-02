using System;

[Serializable]
public class QuestSaveData
{
    public string[] QuestIDs;
    public bool[] QuestAccepted;
    public bool[] QuestCompleted;
    public int[] CurrentStatus;
}