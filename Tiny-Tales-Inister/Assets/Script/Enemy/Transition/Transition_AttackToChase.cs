using UnityEngine;

public class Transition_AttackToChase : FSMTransition
{
    private void Awake()
    {
        Decision = GetComponent<Decision_CanAttackPlayer>();
        FalseState = "Chase";
    }
}
