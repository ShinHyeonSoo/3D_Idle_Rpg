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

    public bool _isDie;

    private void Awake()
    {
        _hp._startValue = GetComponent<Enemy>().Data.hp;
        _hp._maxValue = GetComponent<Enemy>().Data.hp;
    }

    private void Start()
    {
        ExpHandler += CharacterManager.Instance.Player.Condition.IncreaseEXP;
        GoldHandler += InventoryManager.Instance.Currency._goldCoin.Add;
    }

    void Update()
    {
        if (_hp._curValue == 0f && !_isDie)
        {
            Die();
            _isDie = true;
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        _hp.Subtract(damage);
    }

    public void Die()
    {
        // TODO : 사망 애니메이션 재생 후, 삭제로 변경

        Enemy enemy = GetComponent<Enemy>();

        enemy.Animator.SetTrigger("Die");

        EnemySO data = enemy.Data;

        ExpHandler?.Invoke(data.exp);
        GoldHandler?.Invoke(data.goldCoin);
        enemy.RemoveTarget?.Invoke();

        GetComponent<CharacterController>().enabled = false;

        StartCoroutine(CoroutineDie());
    }

    IEnumerator CoroutineDie()
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = gameObject.GetComponent<Enemy>().Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
                break;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
