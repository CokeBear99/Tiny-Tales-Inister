using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string initializeState; // PatrolState
    [SerializeField] private List<FSMState> states;
    [SerializeField] private string[] questID;

    public FSMState CurrentState { get; set; }
    public Transform Player { get; set; }

    public string[] QuestID { get; private set; }

    private void Awake()
    {
        LoadStates();
        QuestID = questID;
    }

    private void LoadStates()
    {
        // 모든 FSMTransition 컴포넌트를 가져옴
        FSMState[] fsmStates = GetComponents<FSMState>();
        // 각 FSMTransition을 Transitions 리스트에 추가
        foreach (FSMState fsmState in fsmStates)
        {
            states.Add(fsmState);
        }
    }

    private void Start()
    {
        ChangeState(initializeState);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void ChangeState(string newStateName)
    {
        FSMState newState = GetState(newStateName);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSMState GetState(string newStateName)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].StateName == newStateName)
            {
                return states[i];
            }
        }

        return null;
    }
}