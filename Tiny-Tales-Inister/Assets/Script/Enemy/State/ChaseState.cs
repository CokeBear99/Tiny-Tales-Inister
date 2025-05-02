using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ChaseState : FSMState
{
    public override void Awake()
    {
        StateName = "Chase";

        Actions.Add(GetComponent<Action_Chase>());
        Transitions.Add(GetComponent<Transition_ChaseToWander>());
        Transitions.Add(GetComponent<Transition_ChaseToAttack>());
    }
}
