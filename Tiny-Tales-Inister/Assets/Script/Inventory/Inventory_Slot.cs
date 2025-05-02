using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;

    [Header("Settings")]
    [SerializeField] private Image item_Icon;
    [SerializeField] private Image item_Quantity_Container;
    [SerializeField] private TextMeshProUGUI item_Quantity_Text;
    
    public int Index { get; set; }

    public void UpdateItemSlot(Item_Base item)
    {
        item_Icon.sprite = item.Icon;
        item_Quantity_Text.text = item.Quantity.ToString();
    }

    public void ShowSlotInfo(bool value)
    {
        item_Icon.gameObject.SetActive(value);
        item_Quantity_Container.gameObject.SetActive(value);
    }


    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index);
    }

}
