using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;

    private int _damage;
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

        //if (other.TryGetComponent(out Health health))
        //{
        //    health.TakeDamage(_damage);
        //}

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
}
