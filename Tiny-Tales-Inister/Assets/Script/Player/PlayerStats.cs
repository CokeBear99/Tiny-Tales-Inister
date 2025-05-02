using BayatGames.SaveGameFree;
using System;
using UnityEngine;

public enum AttributeTypes
{
    STR,
    DEX,
    INT
}

[Serializable]
public class PlayerStatsSaveData
{
    public int Level;
    public float Hp;
    public float Mp;
    public int STR;
    public int DEX;
    public int INT;
    public int AttributePoints;
    public float CurrentExp;
    public float NextLevelExp;
    public float TotalExp;
    public float TotalDamage;
}

[Serializable]
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public event Action OnStatsChanged;
    private readonly string PLAYER_STATS_KEY = "PLAYER_STATS";

    [Header("Level")]
    [SerializeField] private int _level;
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnStatsChanged?.Invoke();
        }
    }

    [Header("HP")]
    [SerializeField] private float _hp;
    public float Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            OnStatsChanged?.Invoke();
        }
    }
    public float MaxHp;

    [Header("MP")]
    [SerializeField] private float _mp;
    public float Mp
    {
        get => _mp;
        set
        {
            _mp = value;
            OnStatsChanged?.Invoke();
        }
    }
    public float MaxMp;

    [Header("Attributes")]
    [SerializeField] private int _str;
    public int STR
    {
        get => _str;
        set
        {
            _str = value;
            OnStatsChanged?.Invoke();
        }
    }

    [SerializeField] private int _dex;
    public int DEX
    {
        get => _dex;
        set
        {
            _dex = value;
            OnStatsChanged?.Invoke();
        }
    }

    [SerializeField] private int _int;
    public int INT
    {
        get => _int;
        set
        {
            _int = value;
            OnStatsChanged?.Invoke();
        }
    }

    [SerializeField] private int _attributePoints;
    public int AttributePoints
    {
        get => _attributePoints;
        set
        {
            _attributePoints = value;
            OnStatsChanged?.Invoke();
        }
    }

    [Header("Exp")]
    [SerializeField] private float _currentExp;
    public float CurrentExp
    {
        get => _currentExp;
        set
        {
            _currentExp = value;
            OnStatsChanged?.Invoke();
        }
    }
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f, 100f)] public float ExpMultiplier;

    [Header("Attack")]
    public float BaseDamage;
    [SerializeField] private float _criticalDamageMultiplier;
    public float CriticalDamageMultiplier
    {
        get => _criticalDamageMultiplier;
        set
        {
            _criticalDamageMultiplier = value;
            OnStatsChanged?.Invoke();
        }
    }

    [SerializeField] private float _criticalChance;
    public float CriticalChance
    {
        get => _criticalChance;
        set
        {
            _criticalChance = value;
            OnStatsChanged?.Invoke();
        }
    }

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



    public void SavePlayerStats()
    {
        PlayerStatsSaveData saveData = new PlayerStatsSaveData
        {
            Level = Level,
            Hp = Hp,
            Mp = Mp,
            STR = STR,
            DEX = DEX,
            INT = INT,
            AttributePoints = AttributePoints,
            CurrentExp = CurrentExp,
            NextLevelExp = NextLevelExp,
            TotalExp = TotalExp,
            TotalDamage = TotalDamage
        };

        SaveGame.Save(PLAYER_STATS_KEY, saveData);
    }

    public void LoadPlayerStats()
    {
        if (SaveGame.Exists(PLAYER_STATS_KEY))
        {
            PlayerStatsSaveData loadData = SaveGame.Load<PlayerStatsSaveData>(PLAYER_STATS_KEY);

            Level = loadData.Level;
            Hp = loadData.Hp;
            Mp = loadData.Mp;
            STR = loadData.STR;
            DEX = loadData.DEX;
            INT = loadData.INT;
            AttributePoints = loadData.AttributePoints;
            CurrentExp = loadData.CurrentExp;
            NextLevelExp = loadData.NextLevelExp;
            TotalExp = loadData.TotalExp;
            TotalDamage = loadData.TotalDamage;

            OnStatsChanged?.Invoke();
        }
    }
}