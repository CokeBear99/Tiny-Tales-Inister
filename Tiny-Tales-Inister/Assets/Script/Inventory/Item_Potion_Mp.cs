using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion_Mp_", menuName = "Items/Potion_Mp")]
public class Item_Potion_Mp : Item_Base
{
    [Header("Settings")]
    public float Mp_RecoveryAmount;

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
        if (GameManager.Instance.Player.PlayerMp.CanRecoverMp())
        {
            GameManager.Instance.Player.PlayerMp.RecoverMp(Mp_RecoveryAmount);
            return true;
        }

        return false;
    }
}
