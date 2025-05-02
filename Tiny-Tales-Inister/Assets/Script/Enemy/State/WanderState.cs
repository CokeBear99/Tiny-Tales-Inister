using UnityEngine;

public class WanderState : FSMState
{
    public override void Awake()
    {
        StateName = "Wander";

        Actions.Add(GetComponent<Action_Wander>());
        Transitions.Add(GetComponent<Transition_WanderToChase>());
    }
}
