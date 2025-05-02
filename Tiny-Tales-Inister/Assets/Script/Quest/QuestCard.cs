using TMPro;
using UnityEngine;

public class QuestCard : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private TextMeshProUGUI questDescriptionText;

    public Quest TargetQuest { get; set; }


    public virtual void SetQuestUI(Quest quest)
    {
        TargetQuest = quest;
        questNameText.text = quest.Name;
        questDescriptionText.text = quest.Descritpion;
    }



}
