using ScottGarland;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamageable
{
    [SerializeField] private Condition _hp;

    private event Action<int> ExpHandler;
    private event Action<int> GoldHandler;

    BigInteger _bigHp;

    private void Awake()
    {
        _hp._startValue = GetComponent<Enemy>().Data.hp;
        _hp._maxValue = GetComponent<Enemy>().Data.hp;

        _bigHp = new BigInteger(GetComponent<Enemy>().Data.bigHp);

        string before = GetComponent<Enemy>().Data.bigHp;
        string after = BigIntegerFormatter.FormatBigInteger(_bigHp);

        Debug.Log(before);
        Debug.Log(after);
    }

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

        EnemySO data = GetComponent<Enemy>().Data;

        ExpHandler?.Invoke(data.exp);
        GoldHandler?.Invoke(data.goldCoin);
    }
}
