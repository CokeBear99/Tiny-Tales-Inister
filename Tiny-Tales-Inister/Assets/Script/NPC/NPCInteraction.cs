using System;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private NPCDialogue dialogueToShow;
    [SerializeField] private GameObject interactionBox;

    [SerializeField] private Quest[] questList;
    [SerializeField] private ShopItem[] shopItemList;
    
    public NPCDialogue DialogueToShow => dialogueToShow;

    private bool isShopNPC;
    private bool isQuestNPC;


    private void Start()
    {
        isQuestNPC = (questList.Length > 0);
        isShopNPC = (shopItemList.Length > 0);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.NPCSelected = this;
            interactionBox.SetActive(true);

            if (questList.Length != 0 && QuestManager.Instance.CurrentQuestNPC == null)
            {
                if (QuestManager.Instance.CurrentQuestNPC == this)
                    return;
                
                QuestManager.Instance.CurrentQuestNPC = this;
                QuestManager.Instance.Quests = questList;
                QuestManager.Instance.LoadQuestsInNPCPanel();
            }

            if (shopItemList.Length != 0 && ShopManager.Instance.CurrentShopNPC == null)
            {
                if (ShopManager.Instance.CurrentShopNPC == this)
                    return;
                
                ShopManager.Instance.CurrentShopNPC = this;                    
                ShopManager.Instance.ItemList = shopItemList;
                ShopManager.Instance.LoadShop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.NPCSelected = null;
                DialogueManager.Instance.CloseDialoguePanel();
            }

            if (interactionBox != null)
                interactionBox.SetActive(false);


            if (isQuestNPC && QuestManager.Instance.CurrentQuestNPC == this)
            {
                QuestManager.Instance.CurrentQuestNPC = null;
                UIManager.Instance.ToggleNPCQuestPanel(false);
            }

            if (isShopNPC && ShopManager.Instance.CurrentShopNPC == this)
            {
                ShopManager.Instance.CurrentShopNPC = null;
                UIManager.Instance.ToggleShopPanel(false);
            }
        }
    }
}
