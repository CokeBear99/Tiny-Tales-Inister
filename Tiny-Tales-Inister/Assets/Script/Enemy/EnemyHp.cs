using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour,IDamageable
{
    [Header("Settings")]
    [SerializeField] private float hp;

    public static event Action OnEnemyDeadEvent;

    public float CurrentHp { get; private set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
        enemyLoot = GetComponent<EnemyLoot>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CurrentHp = hp;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        CurrentHp -= amount;
        if(CurrentHp <= 0)
        {
            EnemyDead();
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    private void EnemyDead()
    {
        animator.SetTrigger("Dead");
        enemyBrain.enabled = false;
        enemySelector.EnemyNoSelectionCallback();
        rb.bodyType = RigidbodyType2D.Static;
        OnEnemyDeadEvent?.Invoke();
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}
