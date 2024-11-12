using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition _uiCondition;

    private float _efficacyStat;
    private float _efficacyRate;
    private float _lastCheckTime;

    private bool _isEfficacy;

    private ConsumableType _consumableType;

    private int _level = 1;

    Condition HP { get { return _uiCondition._hp; } }
    Condition MP { get { return _uiCondition._mp; } }
    Condition EXP { get { return _uiCondition._exp; } }

    void Update()
    {
        RecoveryStamina();

        TempLevelText();

        if (_isEfficacy)
            CheckEfficacy();

        if (HP._curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        HP.Add(amount);
    }

    public void Drink(float amount)
    {
        MP.Add(amount);
    }

    public void IncreaseEXP(int amount)
    {
        EXP.Add(amount);
    }

    public void UseScroll(float amount, float duration)
    {
        _efficacyRate = duration;
        _lastCheckTime = Time.time;

        // TODO : duration �� ���� *= amount ��ŭ ���ݷ� ��� ����
        Debug.Log("��ũ�� ���. ���ݷ� ���� !");

        _isEfficacy = true;
    }

    public void TempLevelText()
    {
        _uiCondition._levelText.text = $"Lv. {_level}";
    }

    private void CheckEfficacy()
    {
        if (Time.time - _lastCheckTime > _efficacyRate)
        {
            // TODO : ������ �����ϴ� ����
            Debug.Log("���� �ð� ����... ��ũ�� ���� ����");

            _isEfficacy = false;
        }
    }

    public void Die()
    {
        Debug.Log("�׾���.");
    }

    public void RecoveryStamina()
    {
        MP.Add(MP._passiveValue * Time.deltaTime);
    }

    public void TakePhysicalDamage(int damage)
    {
        // TODO : ���� ������ �� ���⼭ ����
        //if (_isInvincibility) return;

        HP.Subtract(damage);
        // TODO : Hit �ִϸ��̼� ���

    }

    public bool UseStamina(float amount)
    {
        if (MP._curValue - amount < 0f)
        {
            return false;
        }

        MP.Subtract(amount);
        return true;
    }
}
