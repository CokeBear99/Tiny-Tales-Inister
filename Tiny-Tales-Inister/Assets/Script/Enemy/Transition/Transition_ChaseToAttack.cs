using NUnit.Framework.Interfaces;
using UnityEngine;

public class Transition_ChaseToAttack : FSMTransition
{
    private void Awake()
    {
        Decision = GetComponent<Decision_CanAttackPlayer>();
        TrueState = "Attack";
    }
}
