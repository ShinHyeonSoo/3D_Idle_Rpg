using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;
    public event Action OnDie;

    public bool _isDie = false;

    void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;

        _health = Mathf.Max(_health - damage, 0);

        if (_health == 0)
        {
            _isDie = true;
            OnDie?.Invoke();
        }

        Debug.Log(_health);
    }
}
