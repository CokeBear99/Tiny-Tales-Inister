using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private Player player;


    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.Stats.Hp <= 0)
        {
            PlayerDead();
            player.Stats.Hp = 0;
        }
    }


    public void TakeDamage(float amount)
    {
        if (player.Stats.Hp <= 0) return;

        player.Stats.Hp -= amount;
        DamageManager.Instance.ShowDamageText(amount,player.transform);
    
        if(player.Stats.Hp <= 0)
        {
            PlayerDead();
            player.Stats.Hp = 0;
        }
    }

    private void PlayerDead()
    {
        playerAnimations.ShowDeadAnimation();
    }


    public bool CanRecoverHp()
    {
        return (player.Stats.Hp > 0 && player.Stats.Hp < player.Stats.MaxHp);
    }

    public void RecoverHp(float amount)
    {
        player.Stats.Hp += amount;

        if (player.Stats.Hp > player.Stats.MaxHp)
        {
            player.Stats.Hp += player.Stats.MaxHp;
        }

    }

}
