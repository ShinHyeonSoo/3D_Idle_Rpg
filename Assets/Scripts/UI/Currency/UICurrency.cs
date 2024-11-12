using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrency : MonoBehaviour
{
    public Currency _goldCoin;
    public Currency _silverCoin;

    void Start()
    {
        InventoryManager.Instance.Currency = this;
    }
}
