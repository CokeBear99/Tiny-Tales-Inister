using System;
using UnityEngine;

public class AttributeButton : MonoBehaviour
{
    public static event Action<AttributeTypes> OnAttributesSelectedEvent;

    [Header("Settings")]
    [SerializeField] private AttributeTypes attribute;

    public void SelectedAttribute()
    {
        OnAttributesSelectedEvent?.Invoke(attribute);
    }

}
