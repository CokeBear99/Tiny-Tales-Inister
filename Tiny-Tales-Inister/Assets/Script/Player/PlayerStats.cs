using UnityEngine;

public enum AttributeTypes
{
    STR,
    DEX,
    INT
}


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

    [Header("Attributes")]
    public int STR;
    public int DEX;
    public int INT;
    public int AttributePoints;

    [Header("Exp")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f,100f)]public float ExpMultiplier;

    [Header("Attack")]
    public float BaseDamage;
    public float CriticalDamageMultiplier;
    public float CriticalChance;

    [HideInInspector] public float TotalExp;
    [HideInInspector] public float TotalDamage;


    public void ResetPlayer()
    {
        MaxHp = 20;
        MaxMp = 40;
        Hp = MaxHp;
        Mp = MaxMp;
        Level = 1;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
        TotalExp = 0;
        BaseDamage = 2;
        CriticalChance = 10;
        CriticalDamageMultiplier = 50;
        STR = 0;
        DEX = 0;
        INT = 0;
        AttributePoints = 0;
    }
}
