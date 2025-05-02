using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Waypoint waypoint;
    private Animator animator;
    private Vector3 previousPos;
    private int currentPointIndex = 0;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Vector3 nextPos = waypoint.GetPosition(currentPointIndex);
        UpdateMoveValues(nextPos);
        transform.position = Vector3.MoveTowards
            (transform.position, nextPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPos) <= 0.2f)
        {
            previousPos = nextPos;
            currentPointIndex = (currentPointIndex + 1) % waypoint.Points.Length;
        }

    }


    // NPC 애니메이션 전환 파라미터 값 결정 함수
    private void UpdateMoveValues(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;

        if (previousPos.x < nextPos.x) dir = new Vector2(1f, 0f);
        if (previousPos.x > nextPos.x) dir = new Vector2(-1f, 0f);
        if (previousPos.y < nextPos.y) dir = new Vector2(0f, 1f);
        if (previousPos.y > nextPos.y) dir = new Vector2(0f, -1f);

        if (previousPos.x > nextPos.x && previousPos.y > nextPos.y)
            dir = new Vector2(-1f, 0f);


        animator.SetFloat (moveY, dir.y);
        animator.SetFloat (moveX, dir.x);
    }


}
