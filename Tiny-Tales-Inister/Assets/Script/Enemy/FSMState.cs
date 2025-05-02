using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public abstract class FSMState : MonoBehaviour
{
    public string StateName;
    public List<FSMAction> Actions;
    public List<FSMTransition> Transitions;

    public abstract void Awake();

    public virtual void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }


    public virtual void ExecuteActions()
    {
        for (int i = 0; i< Actions.Count; i++)
        {
            Actions[i].Act();
        }
    }

    public virtual void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Count <= 0)
            return;

        for (int i = 0; i< Transitions.Count;i++)
        {
            bool value = Transitions[i].Decision.Decide();

           if (value)
            {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            }
           else
            {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }

}
