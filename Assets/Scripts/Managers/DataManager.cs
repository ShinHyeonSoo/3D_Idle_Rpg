using ScottGarland;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public PlayerData _playerData;
    public InvenData _invenData;

    private string _savePath;

    protected override void Awake()
    {
        base.Awake();
        _savePath = Application.persistentDataPath;
    }

    public void InsertData()
    {
        _playerData._level = CharacterManager.Instance.Player.Condition.Level;
        _playerData._exp = CharacterManager.Instance.Player.Condition._uiCondition._exp._curValue;
        _playerData._maxExp = CharacterManager.Instance.Player.Condition._uiCondition._exp._maxValue;
        _playerData._hp = CharacterManager.Instance.Player.Condition._uiCondition._hp._curValue;
        _playerData._maxHp = CharacterManager.Instance.Player.Condition._uiCondition._hp._maxValue;
        _playerData._mp = CharacterManager.Instance.Player.Condition._uiCondition._mp._curValue;
        _playerData._maxMp = CharacterManager.Instance.Player.Condition._uiCondition._mp._maxValue;

        _playerData._goldCoin = InventoryManager.Instance.Currency._goldCoin._curValue;
        _playerData._silverCoin = InventoryManager.Instance.Currency._silverCoin._bigValue.ToString();
        _playerData._silverCoinValue = InventoryManager.Instance.Currency._silverCoin._coinValue.ToString();
        _playerData._silverCoinValueCount = InventoryManager.Instance.Currency._silverCoin._getCount;

        foreach (ItemSlot slot in InventoryManager.Instance.Inventory._slots)
        {
            if (slot._item != null)
            {
                SlotData slotData = new SlotData();
                slotData._item = slot._item;
                slotData._quantity = slot._quantity;
                slotData._equipped = slot._equipped;
                _invenData._itemList.Add(slotData);
            }
        }

        foreach (ItemData slot in CharacterManager.Instance.Player.Equipment._curEquips)
        {
            if (slot != null)
            {
                _invenData._equipList.Add(slot);
            }
        }

        SaveData(_playerData);
        SaveData(_invenData);

        Debug.Log("Save 완료");
    }

    public void GetData()
    {
        _playerData = LoadData<PlayerData>();
        _invenData = LoadData<InvenData>();

        CharacterManager.Instance.Player.Condition.Level = _playerData._level;
        CharacterManager.Instance.Player.Condition._uiCondition._exp._curValue = _playerData._exp;
        CharacterManager.Instance.Player.Condition._uiCondition._exp._maxValue = _playerData._maxExp;
        CharacterManager.Instance.Player.Condition._uiCondition._hp._curValue = _playerData._hp;
        CharacterManager.Instance.Player.Condition._uiCondition._hp._maxValue = _playerData._maxHp;
        CharacterManager.Instance.Player.Condition._uiCondition._mp._curValue = _playerData._mp;
        CharacterManager.Instance.Player.Condition._uiCondition._mp._maxValue = _playerData._maxMp;

        InventoryManager.Instance.Currency._goldCoin._curValue = _playerData._goldCoin;
        InventoryManager.Instance.Currency._silverCoin._bigValue = new BigInteger(_playerData._silverCoin);
        InventoryManager.Instance.Currency._silverCoin._coinValue = new BigInteger(_playerData._silverCoinValue);
        InventoryManager.Instance.Currency._silverCoin._getCount = _playerData._silverCoinValueCount;
        InventoryManager.Instance.Currency._goldCoin.UpdateText();
        InventoryManager.Instance.Currency._silverCoin.UpdateBigText();

        int index = 0;
        foreach (SlotData data in _invenData._itemList)
        {
            ItemSlot slot = InventoryManager.Instance.Inventory._slots[index];
            slot._item = data._item;
            slot._quantity = data._quantity;
            slot._equipped = data._equipped;
            ++index;
        }

        foreach (ItemData item in _invenData._equipList)
        {
            CharacterManager.Instance.Player.Equipment.EquipNew(item);
        }

        InventoryManager.Instance.Inventory.UpdateUI();

        Debug.Log("Load 완료");
    }

    public void SaveData<T>(T data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(_savePath + $"/{typeof(T).ToString()}.txt", json);
    }

    public T LoadData<T>()
    {
        string loadJson = File.ReadAllText(_savePath + $"/{typeof(T).ToString()}.txt");
        return JsonUtility.FromJson<T>(loadJson);
    }
}

[Serializable]
public class PlayerData
{
    public int _level;
    public float _exp;
    public float _maxExp;

    public float _hp;
    public float _maxHp;
    public float _mp;
    public float _maxMp;

    public int _goldCoin;
    public string _silverCoin;
    public string _silverCoinValue;
    public int _silverCoinValueCount;
}

[Serializable]
public class InvenData
{
    public List<SlotData> _itemList = new List<SlotData>();

    public List<ItemData> _equipList = new List<ItemData>();
}

[Serializable]
public class SlotData
{
    public ItemData _item;
    public int _quantity;
    public bool _equipped;
}
