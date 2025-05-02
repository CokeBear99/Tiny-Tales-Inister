using System;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;

    [Header("Settings")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private UpgradeSettings[] settings;


    private void UpgradePlayer(int upgradeIndex)
    {
        stats.BaseDamage += settings[upgradeIndex].DamageUpgrade;
        stats.TotalDamage += settings[upgradeIndex].DamageUpgrade;

        stats.MaxHp += settings[upgradeIndex].HpUpgrade;
        stats.MaxMp += settings[upgradeIndex].MpUpgrade;

        stats.CriticalChance += settings[upgradeIndex].CriticalChanceUpgrade;
        stats.CriticalDamageMultiplier += settings[upgradeIndex].CriticalDamageUpgrade;
    }


    private void OnEnable()
    {
        AttributeButton.OnAttributesSelectedEvent += AttributeCallback;
    }

    private void OnDisable()
    {
        AttributeButton.OnAttributesSelectedEvent -= AttributeCallback;
    }


    private void AttributeCallback(AttributeTypes attributeType)
    {
        if (stats.AttributePoints == 0) return;

        switch (attributeType)
        {
            case AttributeTypes.STR:
                UpgradePlayer(0);
                stats.STR++;
                break;

            case AttributeTypes.DEX:
                UpgradePlayer(1);
                stats.DEX++;
                break;

            case AttributeTypes.INT:
                UpgradePlayer(2);
                stats.INT++;
                break;
        }

        stats.AttributePoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }



}


[Serializable]
public class UpgradeSettings
{
    public string Name;

    [Header("values")]
    public float HpUpgrade;
    public float MpUpgrade;
    public float DamageUpgrade;
    public float CriticalChanceUpgrade;
    public float CriticalDamageUpgrade;
}
