using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singletone<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [Header("Settings")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;

    public NPCInteraction NPCSelected { get; set; }

    private bool dialogueStarted;
    private PlayerAction actions;
    private Queue<string> dialogueQueue = new Queue<string>();


    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerAction();
    }

    private void Start()
    {
        actions.Dialogue.Interaction.performed += ctx => ShowDialogue();
        actions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }

    private void ShowDialogue()
    {
        if (NPCSelected == null || dialogueStarted == true || dialoguePanel == null) return;

        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();

        if (npcIcon != null && NPCSelected.DialogueToShow.Icon != null)
            npcIcon.sprite = NPCSelected.DialogueToShow.Icon;

        if (npcNameText != null)
            npcNameText.text = NPCSelected.DialogueToShow.Name;

        if (npcDialogueText != null)
            npcDialogueText.text = NPCSelected.DialogueToShow.Greeting;

        dialogueStarted = true;
    }


    private void ContinueDialogue()
    {
        // NPC와 대화 가능한 범위 밖
        if (NPCSelected == null)
        {
            dialogueQueue.Clear();
            return;
        }
        
        // 큐에 남은 대사 없음
        if (dialogueQueue.Count <= 0)
        {
            CloseDialoguePanel();
            dialogueStarted = false;

            if (NPCSelected.DialogueToShow.HasInteraction)
            {
                OnExtraInteractionEvent?.Invoke(NPCSelected.DialogueToShow.InteractionTYpe);
            }

            return;
        }

        npcDialogueText.text = dialogueQueue.Dequeue();
    }

    private void LoadDialogueFromNPC()
    {
        if (NPCSelected.DialogueToShow.Dialogue.Length <= 0) return;

        foreach (string sentence in NPCSelected.DialogueToShow.Dialogue)
        {
            dialogueQueue.Enqueue(sentence);
        }
    }

    public void CloseDialoguePanel()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
            dialogueStarted = false;
            dialogueQueue.Clear();
        }
    }



    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }




}
