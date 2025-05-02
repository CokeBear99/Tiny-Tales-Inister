using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private TextMeshProUGUI itemName_Text;
    [SerializeField] private TextMeshProUGUI itemCost_Text;
    [SerializeField] private TextMeshProUGUI buyQuantity_Text;
    [SerializeField] private TextMeshProUGUI totalCost_Text;
    [SerializeField] private Image itemIcon_Image;

    private ShopItem item;
    private int quantity;
    private float initialCost;
    private float totalCost;

    public void SetShopCard(ShopItem shopItem)
    {
        item = shopItem;
        // UI
        itemIcon_Image.sprite = shopItem.Item.Icon;
        itemName_Text.text = shopItem.Name;
        itemCost_Text.text = shopItem.Cost.ToString(); // 아이템 1개 가격
        totalCost_Text.text = shopItem.Cost.ToString(); // 아이템 개수 반영한 총 가격 

        // Value Set
        quantity = 1;
        initialCost = shopItem.Cost;
        totalCost = shopItem.Cost;
    }

    
    public void AddQuantity()
    {
        bool canAddQuantity =
            (GoldManager.Instance.Golds >= initialCost * (quantity + 1));

        if (canAddQuantity == true)
        {
            quantity++;
            totalCost = initialCost * quantity;
        }
    }

    public void DecreaseQuantity()
    {
        if (quantity == 1) return;
        
        quantity--;
        totalCost = initialCost * quantity;
    }

    public void BuyItem()
    {
        bool canBuy = GoldManager.Instance.Golds >= totalCost;

        if(canBuy == true)
        {
            GoldManager.Instance.SpendGolds(totalCost);
            Inventory.Instance.AddItem(item.Item, quantity);
            
            quantity = 1;
            totalCost = initialCost;
        }
    }


    private void Update()
    {
        buyQuantity_Text.text = quantity.ToString();
        totalCost_Text.text = totalCost.ToString();
    }


}
