using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
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

        PrintLevelText();

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
        if (EXP.IncreaseMax())
            ++_level;
    }

    public void UseScroll(float amount, float duration)
    {
        _efficacyRate = duration;
        _lastCheckTime = Time.time;

        // duration 초 동안 *= amount 만큼 공격력 상승 로직
        CharacterManager.Instance.Player.Weapon.SetDamageRate(amount);

        _isEfficacy = true;
    }

    public void PrintLevelText()
    {
        _uiCondition._levelText.text = $"Lv. {_level}";
    }

    private void CheckEfficacy()
    {
        if (Time.time - _lastCheckTime > _efficacyRate)
        {
            CharacterManager.Instance.Player.Weapon.SetDamageRate(1f);
            _isEfficacy = false;
        }
    }

    public void Die()
    {
        
    }

    public void RecoveryStamina()
    {
        MP.Add(MP._passiveValue * Time.deltaTime);
    }

    public void TakePhysicalDamage(int damage)
    {
        int armorStats = CharacterManager.Instance.Player.Equipment.EquipStats[(int)EquipType.Armor];
        damage -= Convert.ToInt32(armorStats * 0.5f);
        HP.Subtract(damage);

        CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse();
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
