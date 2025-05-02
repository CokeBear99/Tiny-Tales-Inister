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
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1f);
        }
    }


    public void TakeDamage(float amount)
    {
        player.Stats.Hp -= amount;
    
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


}
