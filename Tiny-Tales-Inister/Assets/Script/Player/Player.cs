using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public PlayerStats Stats => stats;
    public PlayerHp PlayerHp { get; private set; }
    public PlayerMp PlayerMp { get; private set; }


    private PlayerAnimations animations;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
        PlayerHp = GetComponent<PlayerHp>();
        PlayerMp = GetComponent<PlayerMp>();
    }


    public void RespawnPlayer()
    {
        stats.ResetPlayer();
        animations.ResetPlayer();
    }


}
