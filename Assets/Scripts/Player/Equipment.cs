using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public ItemData[] _curEquips;
    public int[] EquipStats { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _curEquips = new ItemData[(int)EquipType.None];
        EquipStats = new int[(int)EquipType.None];
    }

    public void EquipNew(ItemData data)
    {
        UnEquip(data._equipType);
        _curEquips[(int)data._equipType] = data;
        EquipStats[(int)data._equipType] = (int)data._stat;
    }

    public void UnEquip(EquipType type)
    {
        if (_curEquips[(int)type] != null)
        {
            _curEquips[(int)type] = null;
            EquipStats[(int)type] = 0;
        }
    }
}
