using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singletone<DialogueManager>
{
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
        if (NPCSelected == null || dialogueStarted == true) return;

        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();

        npcIcon.sprite = NPCSelected.DialogueToShow.Icon;
        npcNameText.text = NPCSelected.DialogueToShow.Name;
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
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
        dialogueQueue.Clear();
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
