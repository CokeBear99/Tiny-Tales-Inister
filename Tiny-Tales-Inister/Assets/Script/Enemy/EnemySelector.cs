using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject selectorSprite;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void OnEnable()
    {
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += EnemyNoSelectionCallback;
    }

    private void OnDisable()
    {
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= EnemyNoSelectionCallback;
    }

    public void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        if (enemySelected == enemyBrain)
        {
            selectorSprite.SetActive(true);
        }
        else
        {
            selectorSprite.SetActive(false);
        }
    }

    public void EnemyNoSelectionCallback()
    {
        selectorSprite.SetActive(false);
    }
}
