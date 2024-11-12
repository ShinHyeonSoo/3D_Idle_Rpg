using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public ShopSlot[] _slots;
    public Transform _slotPanel;

    public GameObject _selectedTextBundle;
    public GameObject _buyButton;

    private TextMeshProUGUI[] _selectedTexts;

    private int _selectIndex = -1;

    void Start()
    {
        _selectedTexts = _selectedTextBundle.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        gameObject.SetActive(false);

        _slots = new ShopSlot[_slotPanel.childCount];

        for (int i = 0; i < _slots.Length; ++i)
        {
            _slots[i] = _slotPanel.GetChild(i).GetComponent<ShopSlot>();
            _slots[i]._index = i;
            _slots[i]._shop = this;
        }

        ClearSelectedItemWindow();
    }

    private void ClearSelectedItemWindow()
    {
        _selectedTexts[(int)TextType.ItemName].text = string.Empty;
        _selectedTexts[(int)TextType.ItemDescription].text = string.Empty;
        _selectedTexts[(int)TextType.StatName].text = string.Empty;
        _selectedTexts[(int)TextType.StatValue].text = string.Empty;
        _selectedTexts[(int)TextType.ItemPrice].text = string.Empty;

        _buyButton.SetActive(false);
    }

    public void ItemInfoSettings(int index)
    {
        _selectIndex = index;

        if (_slots[_selectIndex]._item == null) return;

        var item = _slots[_selectIndex]._item;

        _selectedTexts[(int)TextType.ItemName].text = item._displayName;
        _selectedTexts[(int)TextType.ItemDescription].text = item._description;

        _selectedTexts[(int)TextType.StatName].text = string.Empty;
        _selectedTexts[(int)TextType.StatValue].text = string.Empty;

        for (int i = 0; i < item._consumables.Length; ++i)
        {
            _selectedTexts[(int)TextType.StatName].text += item._consumables[i]._type.ToString() + "\n";
            _selectedTexts[(int)TextType.StatValue].text += item._consumables[i]._value.ToString() + "\n";
        }

        _selectedTexts[(int)TextType.ItemPrice].text = $"가격 : {item._itemPrice}";

        _buyButton.SetActive(true);
    }

    public void ResetOutLine()
    {
        foreach (var slot in _slots)
        {
            if (slot.gameObject.TryGetComponent(out Outline outline))
            {
                outline.enabled = false;
            }
        }
    }

    public void OnBuyButton()
    {
        if (_selectIndex == -1) return;

        // TODO : 아이템 가격 만큼 재화 차감, 부족하면 return
        if(_slots[_selectIndex]._item._itemPrice > InventoryManager.Instance.Currency._goldCoin._curValue)
        {
            return;
        }
        else
        {
            InventoryManager.Instance.Currency._goldCoin.Subtract(_slots[_selectIndex]._item._itemPrice);
        }

        CharacterManager.Instance.Player._itemData = _slots[_selectIndex]._item;
        CharacterManager.Instance.Player.AddItem?.Invoke();

        if (_slots[_selectIndex].gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }

        ClearSelectedItemWindow();
        _selectIndex = -1;
    }
}
