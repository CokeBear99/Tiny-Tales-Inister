using System.Security;
using UnityEngine;

public class Transition_WanderToChase : FSMTransition
{
    private void Awake()
    {
        Decision = GetComponent<Decision_DetectPlayer>();
        TrueState = "Chase";
    }
}
