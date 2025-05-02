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
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMp(1f);
        }
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


}
