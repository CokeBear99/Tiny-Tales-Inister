using BayatGames.SaveGameFree;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : Singletone<Inventory>
{
    [Header("Settings")]
    [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize;
    [SerializeField] private Item_Base[] inventory_Items;

    public int InventorySize => inventorySize;
    public Item_Base[] Inventory_Items => inventory_Items;

    [Header("Test")]
    [SerializeField] private Item_Base testItem;
    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";

    private void Start()
    {
        inventory_Items = new Item_Base[inventorySize];
        LoadInventory();
        SettingFreeSlot();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 1);
        }
    }

    // 빈 슬롯 처리
    private void SettingFreeSlot()
    {
        for (int i = 0; i < inventory_Items.Length; i++)
        {
            if (inventory_Items[i] == null)
            {
                Inventory_UI.Instance.DrawItem(null, i);
            }
        }
    }

    // 아이템 추가
    public void AddItem(Item_Base item, int quantity)
    {
        if (item == null || quantity == 0)
            return;

        List<int> itemIndexes = CheckItemStock(item.ID);

        // 추가 하려는 아이템이 현재 슬롯에 있을 때
        if (item.IsStackable && itemIndexes.Count > 0 )
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.MaxStack;

                // 현재 아이템이 중첩 가능한 개수보다 작을시
                if (inventory_Items[index].Quantity < maxStack)
                {
                    // 추가하려는 개수를 기존의 슬롯의 개수에 더함
                    inventory_Items[index].Quantity += quantity;
                    
                    // 추가 후 개수가 최대 중첩개수 보다 클 때
                    if (inventory_Items[index].Quantity > maxStack)
                    {
                        int extra = inventory_Items[index].Quantity - maxStack;
                        inventory_Items[index].Quantity = maxStack;
                        AddItem(item, extra);
                    }

                    Inventory_UI.Instance.DrawItem(inventory_Items[index], index);
                    SaveInventory();
                    return;
                }

            }

        }

        // 추가 하려는 아이템이 현재 슬롯에 없을 때
        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
        AddItemToFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }
        SaveInventory();
    }

    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();

        for (int i = 0; i < inventory_Items.Length; i++)
        {
            if (inventory_Items[i] == null)
                continue;

            if (inventory_Items[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    private void AddItemToFreeSlot(Item_Base item,int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory_Items[i] != null)
                continue;

            inventory_Items[i] = item.CopyItem();
            inventory_Items[i].Quantity = quantity;
            Inventory_UI.Instance.DrawItem(inventory_Items[i], i);
            return;
        }
    }

    // 아이템 개수 감소
    private void DecreaseItemStack(int index)
    {
        inventory_Items[index].Quantity--;

        if (inventory_Items[index].Quantity <= 0)
        {
            inventory_Items[index] = null;
        }

        Inventory_UI.Instance.DrawItem(inventory_Items[index], index);
    }

    // 아이템 사용
    public void UseItem(int index)
    {
        Item_Base item = inventory_Items[index];

        if (item == null || item.IsConsumable == false) return;

        if (item.UseItem())
        {
            DecreaseItemStack(index);
        }

        SaveInventory();
    }

    // 아이템 삭제
    public void RemoveItem(int index)
    {
        if (inventory_Items[index] == null) return;

        DecreaseItemStack(index);

        SaveInventory();
    }

    // 아이템 착용
    public void EquipItem(int index)
    {
        Item_Base item = inventory_Items[index];

        if (item == null) return;
        if (item.ItemType != ItemType.Weapon) return;

        item.EquipItem();
    }

    #region 인벤토리 세이브&로드

    public void SaveInventory()
    {
        Inventory_SaveData saveData = new Inventory_SaveData();
        saveData.ItemID = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];

        for (int i = 0; i< inventorySize; i++)
        {
            saveData.ItemID[i] = (inventory_Items[i]) ? inventory_Items[i].ID : null;
            saveData.ItemQuantity[i] = (inventory_Items[i]) ? inventory_Items[i].Quantity : 0;
        }

        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }


    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            Inventory_SaveData loadData = SaveGame.Load<Inventory_SaveData>(INVENTORY_KEY_DATA);

            for (int i = 0; i< inventorySize; i++)
            {
                if (loadData.ItemID[i] != null)
                {
                    Item_Base tempItem = 
                        ItemExistsInGameContent(loadData.ItemID[i]);
                    
                    if (tempItem != null)
                    {
                        inventory_Items[i] = tempItem.CopyItem();
                        inventory_Items[i].Quantity = loadData.ItemQuantity[i];
                        Inventory_UI.Instance.DrawItem(inventory_Items[i],i);
                    }
                    else
                    { // 아이템이 없는 경우
                        inventory_Items[i] = null;
                    }
                }
            }
        }


    }




    private Item_Base ItemExistsInGameContent(string itemID)
    {
        for (int i = 0; i < inventorySize;i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }

        return null;
    }


    #endregion
}
