using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public float Lifetime;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + (transform.forward * Speed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        IDamageable target = collider.gameObject.GetComponent<IDamageable>();
        if (target != null)
            target.TakeDamage(this.gameObject, Damage);
        Destroy(gameObject);
    }
}
