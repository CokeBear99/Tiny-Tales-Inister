using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI damageText;

    public void SetDamageText(float damage)
    {
        damageText.text = damage.ToString();
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
