using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectedEvent;
    public static event Action OnNoSelectionEvent;

    [Header("Settings")]
    [SerializeField] private LayerMask enemyMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast
                (mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyMask);
        
            // 마우스로 선택한 위치에 Enemy 존재시
            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                if (enemy == null) return;
                
                EnemyHp enemyHp = enemy.GetComponent<EnemyHp>();
                if (enemyHp.CurrentHp <= 0f)
                {
                    EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLoot(enemyLoot);
                }
                else
                {
                    OnEnemySelectedEvent.Invoke(enemy);
                }
            }
            // 없을 시
            else
            {
                OnNoSelectionEvent.Invoke();
            }

        }
    }


}
