using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLoot : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private DropItem[] list_CanDropItems;
    [SerializeField] private float expDrop;

    [SerializeField] public List<DropItem> DroppedItems { get; private set; }

    public float ExpDrop => expDrop;


    private void Start()
    {
        LoadDropItems();
    }



    private void LoadDropItems()
    {
        DroppedItems = new List<DropItem>();

        foreach (DropItem item in list_CanDropItems)
        {
            float randomValue = Random.Range(0f, 100f);

            if (randomValue <= item.DropChance)
            {
                DroppedItems.Add(item);
            }
        }

    }
}



[Serializable]
public class DropItem
{
    [Header("Info")]
    public string Name;
    public Item_Base Item;
    public int Quantity;

    [Header("Drop Chance")]
    public float DropChance;
    public bool PickedItem { get; set; }
}
