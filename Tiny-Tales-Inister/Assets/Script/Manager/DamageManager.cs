using System;
using UnityEngine;

public class DamageManager : Singletone<DamageManager>
{
    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowDamageText(float damageAmount, Transform parent)
    {
        DamageText text = Instantiate(damageTextPrefab, parent);
        text.transform.position += Vector3.up * 0.5f;
        text.SetDamageText(damageAmount);
    }
}