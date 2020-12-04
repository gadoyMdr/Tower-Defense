using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 2f)]
    private float baseVelocity = 1.5f;

    private Rigidbody _rigidbody;
    private float damage;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Trigger(float weaponVelocity, float damage)
    {
        this.damage = damage;

        //We also apply a bit of random for the speed so it looks more natural
        _rigidbody.velocity = transform.forward * baseVelocity * weaponVelocity;

        _rigidbody.velocity *= Random.Range(0.85f, 1.15f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
            health.currentHealth -= damage * Random.Range(0.8f, 1.2f);

        Destroy(gameObject);
    }
}