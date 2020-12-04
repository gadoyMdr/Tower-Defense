using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Animations : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() => health.DieEvent += Die;
    

    private void OnDisable() => health.DieEvent -= Die;
    

    void Update()
    {
        Animate();
    }

    void Animate()
    {
        float y = Mathf.Min(_rigidbody.velocity.magnitude, 1);

        _animator.SetFloat("Y", y);
    }

    public void Die()
    {
        _rigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Animations>().enabled = false;
        GetComponent<MyNavMeshAgent>().enabled = false;
        GetComponent<AIBrain>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        _animator.SetTrigger("Dying" + Random.Range(1, 4));
    }
    
}
