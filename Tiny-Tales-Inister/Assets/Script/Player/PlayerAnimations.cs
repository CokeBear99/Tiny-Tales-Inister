using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int revive = Animator.StringToHash("Revive");

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowDeadAnimation()
    {
        animator.SetTrigger(dead);
    }

    public void SetMovingAnimation(bool value,Vector2 dir)
    {
        animator.SetBool(moving, value);
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }

    public void ResetPlayer()
    {
        SetMovingAnimation(false,Vector2.down);
        animator.SetTrigger(revive);
    }


}
