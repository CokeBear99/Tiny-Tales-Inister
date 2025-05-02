using UnityEngine;

public class Action_Chase : FSMAction
{
    [Header("Config")]
    [SerializeField] private float chaseSpeed;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();   
    }

    public override void Act()
    {
        ChasePlayer();
    }


    private void ChasePlayer()
    {
        if (enemyBrain.Player == null) return;

        Vector3 distanceToPlayer = enemyBrain.Player.position - transform.position;
        
        if (distanceToPlayer.magnitude >= 1f)
        {
            transform.Translate(distanceToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }




    }
}
