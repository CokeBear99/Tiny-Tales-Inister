using UnityEngine;

public class PlayerMp : MonoBehaviour
{
    private Player player;



    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {

    }


    public bool UseMp(float amount)
    {
        if(player.Stats.Mp >= amount)
        {
            player.Stats.Mp = Mathf.Max(player.Stats.Mp -= amount, 0);
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool CanRecoverMp()
    {
        return player.Stats.Mp >= 0 && player.Stats.Mp < player.Stats.MaxMp;
    }


    public void RecoverMp(float amount)
    {
        player.Stats.Mp += amount;

        // 회복 후 마나가 MaxMp 초과시 Max 값으로 조정
        player.Stats.Mp = Mathf.Min(player.Stats.Mp , player.Stats.MaxMp);
    }


}
