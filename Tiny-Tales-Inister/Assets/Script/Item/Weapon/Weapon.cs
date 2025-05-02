using UnityEngine;

public enum WeaponType
{
    Magic,
    Melee
}


[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    [Header("Settings")]
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;

    [Header("Projectile Info")]
    public Projectile ProjectilePrefab;
    public float RequiredMana;

}
