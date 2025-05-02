using UnityEditor.Search;
using UnityEngine;

public class ShopManager : Singletone<ShopManager>
{
    [Header("Settings")]
    [SerializeField] private ShopCard shopCardPrefab;
    [SerializeField] private Transform shopContainer;

    [Header("Shop Item List")]
    [SerializeField] private ShopItem[] itemList;

    private void Start()
    {
        LoadShop();
    }

    private void LoadShop()
    {
        for (int i = 0; i< itemList.Length; i++)
        {
            ShopCard card = Instantiate(shopCardPrefab, shopContainer);
            card.SetShopCard(itemList[i]);
        }
    }


}
