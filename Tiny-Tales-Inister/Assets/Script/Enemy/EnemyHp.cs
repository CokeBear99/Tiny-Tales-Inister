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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
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
            animator.SetTrigger("Dead");
            enemyBrain.enabled = false;
            enemySelector.EnemyNoSelectionCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            OnEnemyDeadEvent?.Invoke();
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }
}
