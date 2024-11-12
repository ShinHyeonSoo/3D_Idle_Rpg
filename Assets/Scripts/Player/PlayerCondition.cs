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

    //private float _efficacyStat;
    //private float _efficacyRate;
    //private float _lastCheckTime;

    //private bool _isEfficacy;
    //private bool _isInvincibility;

    //private ConsumableType _consumableType;

    private int _level = 1;

    Condition HP { get { return _uiCondition._hp; } }
    Condition MP { get { return _uiCondition._mp; } }
    Condition EXP { get { return _uiCondition._exp; } }

    void Update()
    {
        RecoveryStamina();

        TempLevelText();

        //if (_isEfficacy)
        //    CheckEfficacy();

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

    public void TempLevelText()
    {
        _uiCondition._levelText.text = $"Lv. {_level}";
    }

    //public void SpeedBoost(float amount, float duration, ConsumableType type)
    //{
    //    _efficacyStat = amount;
    //    CharacterManager.Instance.Player.Controller._moveSpeed *= _efficacyStat;
    //    _efficacyRate = duration;
    //    _consumableType = type;
    //    _lastCheckTime = Time.time;
    //    _isEfficacy = true;
    //}

    //public void DoubleJump(float duration, ConsumableType type)
    //{
    //    CharacterManager.Instance.Player.Controller.IsDoubleJump = true;
    //    _efficacyRate = duration;
    //    _consumableType = type;
    //    _lastCheckTime = Time.time;
    //    _isEfficacy = true;
    //}

    //public void Invincibility(float duration, ConsumableType type)
    //{
    //    _isInvincibility = true;
    //    _efficacyRate = duration;
    //    _consumableType = type;
    //    _lastCheckTime = Time.time;
    //    _isEfficacy = true;
    //}

    //private void CheckEfficacy()
    //{
    //    if (Time.time - _lastCheckTime > _efficacyRate)
    //    {
    //        switch (_consumableType)
    //        {
    //            case ConsumableType.SpeedBoost:
    //                // TODO : 아이템 먹었을 때와, 달리기 중일때 구분해서 속도 되돌리기
    //                CharacterManager.Instance.Player.Controller.RestoreSpeed(_efficacyStat);
    //                _efficacyStat = 0f;
    //                break;
    //            case ConsumableType.DoubleJump:
    //                CharacterManager.Instance.Player.Controller.IsDoubleJump = false;
    //                break;
    //            case ConsumableType.Invincibility:
    //                _isInvincibility = false;
    //                break;
    //        }
    //        _consumableType = ConsumableType.None;
    //        _isEfficacy = false;
    //    }
    //}

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
