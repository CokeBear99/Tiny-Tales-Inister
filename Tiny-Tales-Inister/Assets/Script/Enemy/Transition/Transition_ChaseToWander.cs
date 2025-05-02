using UnityEngine;

public class Transition_ChaseToWander : FSMTransition
{
    private void Awake()
    {
        Decision = GetComponent<Decision_DetectPlayer>();
        FalseState = "Wander";
    }

}
