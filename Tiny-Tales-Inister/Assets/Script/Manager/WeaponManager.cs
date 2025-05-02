using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singletone<WeaponManager> 
{
    [Header("Confiog")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponMpText;

    public void EquipWeapon(Weapon weapon)
    {
        weaponIcon.sprite = weapon.Icon;
        weaponIcon.gameObject.SetActive(true);
        weaponMpText.text = weapon.RequiredMana.ToString();
        weaponMpText.gameObject.SetActive(true);
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }

}
