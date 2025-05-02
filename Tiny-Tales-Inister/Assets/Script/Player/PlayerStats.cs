using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats",menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int Level;

    [Header("MP")]
    public float Hp;
    public float MaxHp;
    
    [Header("MP")]
    public float Mp;
    public float MaxMp;

    [Header("Exp")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f,100f)]public float ExpMultiplier;

    public void ResetPlayer()
    {
        Hp = MaxHp;
        Mp = MaxMp;
        Level = 1;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
    }
}
