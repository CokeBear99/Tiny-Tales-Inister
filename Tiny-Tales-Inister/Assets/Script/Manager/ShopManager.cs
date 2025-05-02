using UnityEditor.Search;
using UnityEngine;

public class ShopManager : Singletone<ShopManager>
{
    [Header("Settings")]
    [SerializeField] private ShopCard shopCardPrefab;
    [SerializeField] private Transform shopContainer;

    [Header("Shop Item List")]
    public ShopItem[] ItemList { get; set; }

    public NPCInteraction CurrentShopNPC { get; set; }



    public void LoadShop()
    {
        ClearShopPanel();

        for (int i = 0; i< ItemList.Length; i++)
        {
            ShopCard card = Instantiate(shopCardPrefab, shopContainer);
            card.SetShopCard(ItemList[i]);
        }
    }

    public void ClearShopPanel()
    {
        if (shopContainer == null) return;

        int childCount = shopContainer.childCount;

        if (childCount <= 0) return;

        // 생성되어 있는 상점 아이템 카드 모두 삭제
        for (int i = 0; i < childCount; i++)
        {
            Transform child = shopContainer.GetChild(i);
            Destroy(child.gameObject);
        }
    }


}
