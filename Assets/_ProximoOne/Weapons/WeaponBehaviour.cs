using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnLocations;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _fireRate = 2f;

    private float _nextFireTime = 0;

    public void Fire()
    {
        if (Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + (1 / _fireRate);
        foreach (Transform spawn in _spawnLocations)
            Instantiate(_projectile, spawn.position, spawn.rotation);
    }
}
