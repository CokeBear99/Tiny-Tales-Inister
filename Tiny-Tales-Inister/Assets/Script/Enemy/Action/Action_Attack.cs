using UnityEngine;

public class Action_Attack : FSMAction
{
    [Header("Settings")]
    [SerializeField] private float damage;
    [SerializeField] private float attackDelay;

    private EnemyBrain enemyBrain;
    private float timer;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (enemyBrain.Player == null)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            IDamageable player = enemyBrain.Player.GetComponent<IDamageable>();
            player.TakeDamage(damage);
            timer = attackDelay;
        }
    }



}
