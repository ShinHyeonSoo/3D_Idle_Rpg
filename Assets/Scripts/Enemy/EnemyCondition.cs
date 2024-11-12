using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamageable
{
    [SerializeField] private Condition _hp;

    private event Action<int> ExpHandler;
    private event Action<int> GoldHandler;

    private void Start()
    {
        ExpHandler += CharacterManager.Instance.Player.Condition.IncreaseEXP;
        GoldHandler += InventoryManager.Instance.Currency._goldCoin.Add;
    }

    void Update()
    {
        if (_hp._curValue == 0f)
        {
            Die();
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        _hp.Subtract(damage);
    }

    public void Die()
    {
        // TODO : ��� �ִϸ��̼� ��� ��, ������ ����
        Destroy(gameObject);

        EnemySO data = GetComponent<Enemy>().Data;

        ExpHandler?.Invoke(data.exp);
        GoldHandler?.Invoke(data.goldCoin);
    }
}
