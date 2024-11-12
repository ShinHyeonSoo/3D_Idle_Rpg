using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
}

public enum ConsumableType
{
    HP,
    MP,
    EXP,
    Scroll,
    None
}

public enum EquipType
{
    Weapon,
    Armor,
    None
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType _type;
    public float _value;
    public float _duration;
}

[CreateAssetMenu(fileName = "Item", menuName = "Create Data/New Item", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string _displayName;
    public string _description;
    public int _itemCode;
    public int _itemPrice;
    public ItemType _type;
    public Sprite _icon;

    [Header("Stacking")]
    public bool _canStack;
    public int _maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] _consumables;

    [Header("Equip")]
    public EquipType _equipType;
    public float _stat;
}
