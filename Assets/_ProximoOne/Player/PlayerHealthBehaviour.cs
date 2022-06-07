using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(GameObject source, int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }
}
