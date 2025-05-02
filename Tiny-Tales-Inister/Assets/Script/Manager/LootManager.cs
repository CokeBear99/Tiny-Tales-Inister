using UnityEngine;

public class LootManager : Singletone<LootManager>
{
    [Header("Settings")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonprefab;
    [SerializeField] private Transform container;


    public void ShowLoot(EnemyLoot enemyLoot)
    {
        lootPanel.SetActive(true);
        if (LootPanelWithItems())
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        foreach (DropItem item in enemyLoot.DroppedItems)
        {
            if (item.PickedItem) continue;

            LootButton lootButton = Instantiate(lootButtonprefab, container);
            lootButton.SetLootButton(item);
        }
    }



    private bool LootPanelWithItems()
    {
        return container.childCount > 0;
    }

    // Loot 패널 닫기 버튼
    public void CloseLootPanel()
    {
        lootPanel.SetActive(false);
    }


}
