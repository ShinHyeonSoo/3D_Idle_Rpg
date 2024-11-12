using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;

    private int _damage;
    private float _damageRate = 1f;
    private float _knockback;

    private List<Collider> _alreadyCollider = new List<Collider>();

    private void OnEnable()
    {
        _alreadyCollider.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _myCollider) return;
        if (_alreadyCollider.Contains(other)) return;

        _alreadyCollider.Add(other);

        if (other.TryGetComponent(out IDamageable damage))
        {
            damage.TakePhysicalDamage(Convert.ToInt32(_damage * _damageRate));
        }

        //if (other.TryGetComponent(out ForceReceiver force))
        //{
        //    Vector3 dir = (other.transform.position - _myCollider.transform.position);
        //    force.AddForce(dir * _knockback);
        //}
    }

    public void SetAttack(int damage, float knockback)
    {
        this._damage = damage;
        this._knockback = knockback;
    }
    
    public void SetDamageRate(float damageRate)
    {
        this._damageRate = damageRate;
    }
}
