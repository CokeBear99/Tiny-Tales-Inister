using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion_Hp_", menuName = "Items/Potion_Hp")]
public class Item_Potion_Hp : Item_Base
{
    [Header("Settings")]
    public float Hp_RecoveryAmount;

    public override void EquipItem()
    {
        base.EquipItem();
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
    }

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerHp.CanRecoverHp())
        {
            GameManager.Instance.Player.PlayerHp.RecoverHp(Hp_RecoveryAmount);
            return true;
        }

        return false;
    }
}
