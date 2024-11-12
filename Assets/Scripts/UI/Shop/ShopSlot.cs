using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ItemData _item;
    public UIShop _shop;

    public Image _icon;
    private Outline _outLine;

    public int _index;

    void Awake()
    {
        _outLine = GetComponent<Outline>();
        _icon.sprite = _item._icon;
    }

    public void Setting()
    {
        _shop.ResetOutLine();

        _outLine.enabled = true;

        _shop.ItemInfoSettings(_index);
    }

    public void OnClickButton()
    {
        Setting();
    }
}
