using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 10;

    public UnityEvent OnDeath;

    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(GameObject source, int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (_health <= 0)
            OnDeath?.Invoke();
    }
}
