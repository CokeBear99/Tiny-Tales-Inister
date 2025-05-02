using UnityEngine;

public class BoundIndicator : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (boxCollider2D == null)
        {
            Debug.LogError("BoxCollider2D가 오브젝트에 없습니다!");
        }
    }

    // OnDrawGizmos는 Scene 뷰에서만 호출되어 시각적 디버깅 요소를 그립니다
    void OnDrawGizmos()
    {
        DrawColliderGizmo();
    }

    // OnDrawGizmosSelected는 오브젝트가 선택되었을 때만 호출됩니다
    void OnDrawGizmosSelected()
    {
        DrawColliderGizmo();
    }

    // 콜라이더 Gizmo를 그리는 함수
    void DrawColliderGizmo()
    {
        // Start 함수가 아직 호출되지 않은 경우 (에디터 모드에서)
        if (boxCollider2D == null)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();

            if (boxCollider2D == null)
            {
                return; // 콜라이더가 없으면 그리지 않음
            }
        }

        // Gizmo 색상 설정 (녹색으로)
        Gizmos.color = Color.green;

        // 콜라이더의 위치와 크기 계산
        Vector2 offset = boxCollider2D.offset;
        Vector2 size = boxCollider2D.size;

        // 회전된 크기 적용을 위한 행렬
        Gizmos.matrix = transform.localToWorldMatrix;

        // 박스의 윤곽선 (와이어프레임)만 그리기
        Gizmos.DrawWireCube(offset, size);
    }

}
