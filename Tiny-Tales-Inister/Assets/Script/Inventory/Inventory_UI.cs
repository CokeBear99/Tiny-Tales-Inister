using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_UI : Singletone<Inventory_UI>
{
    [Header("Settings")]
    [SerializeField] private Inventory_Slot slotPrefab;
    [SerializeField] private Transform container;

    [Header("Description Panel")]
    [SerializeField] private GameObject description_Panel;
    [SerializeField] private Image ItemIcon_Image;
    [SerializeField] private TextMeshProUGUI ItemName_Text;
    [SerializeField] private TextMeshProUGUI ItemDescription_Text;




    private List<Inventory_Slot> slotList = new List<Inventory_Slot>();

    public Inventory_Slot CurrentSlot { get; set; }


    protected override void Awake()
    {
        base.Awake();
        InitializeInventory();
    }


    private void InitializeInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            Inventory_Slot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    public void DrawItem(Item_Base item,int index)
    {
        Inventory_Slot slot = slotList[index];

        if (item == null)
        {
            slot.ShowSlotInfo(false);
            return;
        }

        slot.ShowSlotInfo(true);
        slot.UpdateItemSlot(item);
    }


    #region SlotSelected Subscribe && Keyboard Control Toggle

    private void OnEnable()
    {
        Inventory_Slot.OnSlotSelectedEvent += SlotSelectedCallback;
    }

    private void OnDisable()
    {
        Inventory_Slot.OnSlotSelectedEvent -= SlotSelectedCallback;
    }

    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(CurrentSlot.Index);
    }


    #endregion

    #region Inventory Button 

    // 아이템 사용 함수 (Inventory.cs의 UseItem 참조)
    public void UseItem() => Inventory.Instance.UseItem(CurrentSlot.Index);

    // 아이템 제거 함수
    public void RemoveItem()
    {
        if (CurrentSlot == null) return;

        Inventory.Instance.RemoveItem(CurrentSlot.Index);
    }

    // 아이템 착용 함수
    public void EquipItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.EquipItem(CurrentSlot.Index);
    }

    #endregion

   
    // Description Panel 

    private void ShowItemDescription(int index)
    {
        Item_Base item = Inventory.Instance.Inventory_Items[index];

        if (item == null)
        {
            description_Panel.SetActive(false);
            return;
        }

            description_Panel.SetActive(true);
        ItemIcon_Image.sprite = item.Icon;
        ItemName_Text.text = item.Name;
        ItemDescription_Text.text = item.Description;
    }


}
