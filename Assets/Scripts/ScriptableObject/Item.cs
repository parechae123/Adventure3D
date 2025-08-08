using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = int.MaxValue)]
[Serializable]
public class Item : ScriptableObject
{
    [SerializeField] int itemCode;
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField] private string description;
    public string Description { get { return description; } }

    public int ItemCode { get { return itemCode; } }

    [SerializeField] int itemValue;
    public int ItemValue { get { return itemValue; } }

    [SerializeField] float duration;


    public float Duration { get { return duration; } }
    
    [SerializeField] ItemType itemType;
    public ItemType ItemType { get { return itemType; } }
    [SerializeField] Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
}
public enum ItemType { posion }