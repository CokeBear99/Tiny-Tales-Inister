using UnityEngine;

public class GameManager : Singletone<GameManager>
{

    [SerializeField] private Player player;

    public Player Player => player;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            player.RespawnPlayer();
    }

    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }


}
