using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamageable
{
    [SerializeField] private Condition _hp;
    [SerializeField] private int _exp;
    [SerializeField] private int _goldCoin;

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
        // TODO : 사망 애니메이션 재생 후, 삭제로 변경
        Destroy(gameObject);
        ExpHandler?.Invoke(_exp);
        GoldHandler?.Invoke(_goldCoin);
    }
}
