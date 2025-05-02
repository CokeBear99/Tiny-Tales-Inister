using NUnit.Framework.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed;

    private PlayerAnimations playerAnimations;
    private PlayerAction actions;
    private Player player;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection = Vector2.down;

    public Vector2 MoveDirection => moveDirection;

    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerAction();
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }


    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;

        if(moveDirection == Vector2.zero)
        {
            playerAnimations.SetMovingAnimation(false,lastMoveDirection);
            return;
        }

        lastMoveDirection = moveDirection;
        playerAnimations.SetMovingAnimation(true, moveDirection);
    }


    private void Move()
    {
        if (player.Stats.Hp <= 0)
            return;

        rb.MovePosition(rb.position + moveDirection * (speed * Time.fixedDeltaTime));
    }


}
