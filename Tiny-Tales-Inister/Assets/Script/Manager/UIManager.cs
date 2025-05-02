using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("Bars")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private Image expBar;

    [Header("Player UI Texts")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI expText;

    [Header("Stats Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI stat_Level_Text;
    [SerializeField] private TextMeshProUGUI stat_Damage_Text;
    [SerializeField] private TextMeshProUGUI stat_CriticalChance_Text;
    [SerializeField] private TextMeshProUGUI stat_CriticalDamage_Text;
    [SerializeField] private TextMeshProUGUI stat_TotalExp_Text;
    [SerializeField] private TextMeshProUGUI stat_CurrentExp_Text;
    [SerializeField] private TextMeshProUGUI stat_RequiredExp_Text;

    [Header("Attributes Panel")]
    [SerializeField] private TextMeshProUGUI attribute_Points_Text;
    [SerializeField] private TextMeshProUGUI attribute_STR_Text;
    [SerializeField] private TextMeshProUGUI attribute_DEX_Text;
    [SerializeField] private TextMeshProUGUI attribute_INT_Text;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject inventoryPanel;


    private void Update()
    {
        UpdatePlayerUI();
    }



    public void ToggleStatsPanel()
    {
        bool isStatsVisible = statsPanel.activeSelf;
        statsPanel.SetActive(!isStatsVisible);

        if (!isStatsVisible)  // 패널이 새로 열렸을 때만 업데이트
        {
            UpdateStatsPanel();
        }
    }

    public void ToggleInventoryPanel()
    {
        bool isInventoryVisible = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isInventoryVisible);
    }


    #region Value Update


    private void UpdatePlayerUI()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, stats.Hp / stats.MaxHp, 10f * Time.deltaTime);
        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, stats.Mp / stats.MaxMp, 10f * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelExp, 10f * Time.deltaTime);

        levelText.text = $"Level {stats.Level}";
        hpText.text = $"{stats.Hp} / {stats.MaxHp}";
        mpText.text = $"{stats.Mp} / {stats.MaxMp}";
        expText.text = $"{stats.CurrentExp} / {stats.NextLevelExp}";
    }

    private void UpdateStatsPanel()
    {
        stat_Level_Text.text = stats.Level.ToString();
        stat_Damage_Text.text = stats.TotalDamage.ToString();
        stat_CriticalChance_Text.text = stats.CriticalChance.ToString();
        stat_CriticalDamage_Text.text = stats.CriticalDamageMultiplier.ToString();
        stat_TotalExp_Text.text = stats.TotalExp.ToString();
        stat_CurrentExp_Text.text = stats.CurrentExp.ToString();
        stat_RequiredExp_Text.text = stats.NextLevelExp.ToString();

        attribute_Points_Text.text = stats.AttributePoints.ToString();
        attribute_STR_Text.text = stats.STR.ToString();
        attribute_DEX_Text.text = stats.DEX.ToString();
        attribute_INT_Text.text = stats.INT.ToString();
    }


    #endregion


    #region Attribute Update


    private void OnEnable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent += UpgradeCallback;
    }

    private void OnDisable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent -= UpgradeCallback;
    }

    private void UpgradeCallback()
    {
        UpdateStatsPanel();
    }

    #endregion
}
