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

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI expText;

    private void Update()
    {
        UpdatePlayerUI();
    }

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




}
