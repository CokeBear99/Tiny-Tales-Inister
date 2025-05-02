using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions;

    [Header("Melee Attack Settings")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapon CurrentWeapon { get; set; }

    private PlayerMovement playerMovement;
    private PlayerAction actions;
    private PlayerAnimations playerAnimations;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;
    private PlayerMp playerMp;
    private Player player => GetComponent<Player>();

    // Projectiles Shoot 
    private Transform currentAttackPosition;
    private float currentAttackRotation;

    private void Awake()
    {
        actions = new PlayerAction();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMp = GetComponent<PlayerMp>();
    }

    private void Start()
    {
        EquipWeapon(initialWeapon);

        // ClickAttack에 할당된 키가 입력됐을 때, Attack() 함수 실행
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetShootPosition();
    }

    #region Target Subscribe

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoEnemySelectionCallback()
    {
        enemyTarget = null;
    }


    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += NoEnemySelectionCallback;
        EnemyHp.OnEnemyDeadEvent += NoEnemySelectionCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= NoEnemySelectionCallback;
        EnemyHp.OnEnemyDeadEvent -= NoEnemySelectionCallback;
    }

    #endregion

    private void Attack()
    {
        if (enemyTarget == null)
        {
            return;
        }

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEAttack());
    }

    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null)
        {
            yield break;
        }

        if (CurrentWeapon.WeaponType == WeaponType.Magic)
        {
            if (playerMp.UseMp(CurrentWeapon.RequiredMana) == false)
            {
                yield break;
            }

            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }

        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void MagicAttack()
    {
        // 발사체 방향 
        Quaternion rotation =
            Quaternion.Euler(new Vector3(0, 0, currentAttackRotation));

        // 생성
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab,
            currentAttackPosition.position, rotation);

        projectile.Direction = Vector3.up;
        projectile.Damage = GetAttackDamage();
    }

    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();

        float currentDistanceToEnemy =
            Vector3.Distance(enemyTarget.transform.position, transform.position);

        if (currentDistanceToEnemy <= minDistanceMeleeAttack)
        {
            enemyTarget.GetComponent<IDamageable>()?.TakeDamage(GetAttackDamage());
        }
    }


    #region Projectiles

    private void GetShootPosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;

        // 좌우
        switch(moveDirection.x)
        {
            case > 0f:
                currentAttackPosition = attackPositions[1];
                currentAttackRotation = -90f;
                break;

            case < 0f:
                currentAttackPosition = attackPositions[3];
                currentAttackRotation = -270f;
                break;
        }

        // 위 아래
        switch (moveDirection.y)
        {
            case > 0f:
                currentAttackPosition = attackPositions[0];
                currentAttackRotation = 0f;
                break;

            case < 0f:
                currentAttackPosition = attackPositions[2];
                currentAttackRotation = -180f;
                break;
        }
    }


    #endregion





    private float GetAttackDamage()
    {
        float damage = stats.BaseDamage;
        damage += CurrentWeapon.Damage;
        float randomPercentage = Random.Range(0f, 100);

        if(randomPercentage <= stats.CriticalChance)
        {
            damage += damage * (stats.CriticalDamageMultiplier / 100f);
        }

        return damage;
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        player.Stats.TotalDamage = stats.BaseDamage + CurrentWeapon.Damage;
    }


}
