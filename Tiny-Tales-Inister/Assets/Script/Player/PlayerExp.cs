using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    
    private Player player => GetComponent<Player>();

    public void AddExp(float amount)
    {
        player.Stats.TotalExp += amount;
        player.Stats.CurrentExp += amount;

        while(player.Stats.CurrentExp >= player.Stats.NextLevelExp)
        {
            player.Stats.CurrentExp -= player.Stats.NextLevelExp;

            NextLevel();
        }
    }


    private void NextLevel()
    {
        // 레벨업
        player.Stats.Level++;
        player.Stats.AttributePoints++;
        
        // 레벨업 후, 다음 레벨 필요 Exp
        float currentExpRequired = player.Stats.NextLevelExp;
        float newNextLevelExp =
            Mathf.Round(currentExpRequired + currentExpRequired 
            * (player.Stats.ExpMultiplier / 100f));

        player.Stats.NextLevelExp = newNextLevelExp;
    }
}
