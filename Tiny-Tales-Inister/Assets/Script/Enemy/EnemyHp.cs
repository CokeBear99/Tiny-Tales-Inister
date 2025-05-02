using System;
using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour,IDamageable
{
    [Header("Settings")]
    [SerializeField] private float hp;
    [SerializeField] private Vector2 respawnTimeRange = new Vector2(20f, 30f); // 부활 시간 범위

    public static event Action OnEnemyDeadEvent;

    public float CurrentHp { get; private set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;
    private Rigidbody2D rb;
    private Vector3 initialPosition; // 초기 위치 저장
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
        enemyLoot = GetComponent<EnemyLoot>();
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // 초기 위치 저장
    }

    void Start()
    {
        CurrentHp = hp;
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
        if (isDead) return; // 이미 죽었으면 중복 실행 방지

        isDead = true;
        animator.SetTrigger("Dead");
        enemyBrain.enabled = false;
        enemySelector.EnemyNoSelectionCallback();
        rb.bodyType = RigidbodyType2D.Static;
        OnEnemyDeadEvent?.Invoke();
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);

        AddQuestProgress();

        // 랜덤 부활 시간 설정
        float randomRespawnTime = UnityEngine.Random.Range(respawnTimeRange.x, respawnTimeRange.y);
        StartCoroutine(RespawnCoroutine(randomRespawnTime));
    }

    private void AddQuestProgress()
    {
        // 퀘스트 진척도
        foreach (string questID in enemyBrain.QuestID)
        {
            QuestManager.Instance.AddProgress(questID, 1);
        }
    }

    private IEnumerator RespawnCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 적 부활 처리
        RespawnEnemy();
    }

    private void RespawnEnemy()
    {
        // 상태 초기화
        CurrentHp = hp;
        isDead = false;

        // 물리 상태 및 위치 초기화
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = initialPosition;

        // 애니메이션 상태 초기화
        animator.ResetTrigger("Dead");

        // 애니메이션 상태를 Walk로 변경 (Enemy_Walk 상태로 전환)
        animator.Play("Enemy_Walk");

        // EnemyBrain 컴포넌트 활성화 및 초기 상태로 변경
        enemyBrain.enabled = true;
        enemyBrain.ChangeState("Wander");
    }

}
