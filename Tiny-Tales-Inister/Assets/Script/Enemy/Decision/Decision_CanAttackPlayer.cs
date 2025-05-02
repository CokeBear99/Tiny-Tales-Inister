using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision_CanAttackPlayer : FSMDecision
{
    [Header("Settings")]
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange()
    {
        if (enemy.Player == null) return false;

        Collider2D playerCollider =
            Physics2D.OverlapCircle(enemy.transform.position,attackRange, playerMask);

        if (playerCollider != null)
        {
            Debug.Log("CanAttack");
            return true;
        }

        Debug.Log("Can Not Attack");
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
