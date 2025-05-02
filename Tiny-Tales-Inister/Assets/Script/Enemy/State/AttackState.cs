using UnityEngine;

public class AttackState : FSMState
{
    public override void Awake()
    {
        StateName = "Attack";

        Actions.Add(GetComponent<Action_Attack>());

        Transitions.Add(GetComponent<Transition_AttackToChase>());
    }
}
