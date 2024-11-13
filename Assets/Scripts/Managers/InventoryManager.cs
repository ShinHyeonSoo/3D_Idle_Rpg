using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public UICurrency Currency { get; set; }
    public UIInventory Inventory { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }
}
