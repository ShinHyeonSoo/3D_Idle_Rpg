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

        // TODO : duration 초 동안 *= amount 만큼 공격력 상승 로직
        Debug.Log("스크롤 사용. 공격력 증가 !");

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
            // TODO : 버프를 해제하는 로직
            Debug.Log("버프 시간 종료... 스크롤 버프 삭제");

            _isEfficacy = false;
        }
    }

    public void Die()
    {
        Debug.Log("죽었다.");
    }

    public void RecoveryStamina()
    {
        MP.Add(MP._passiveValue * Time.deltaTime);
    }

    public void TakePhysicalDamage(int damage)
    {
        // TODO : 무적 상태일 때 여기서 리턴
        //if (_isInvincibility) return;

        HP.Subtract(damage);
        // TODO : Hit 애니메이션 재생

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
