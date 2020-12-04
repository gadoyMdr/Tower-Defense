using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PikesDamage : MonoBehaviour, IActionable
{
    [SerializeField]
    [Range(1f, 200f)]
    private float health = 50;

    [SerializeField]
    [Range(1f, 200f)]
    private float damage = 10;

    [SerializeField]
    [Range(1f, 200f)]
    private float selfDamage = 5;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float damageDelay = 0.3f;

    private Health _health;

    private bool canDamage = true;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void TriggerAction(Transform transformToFocus, bool value)
    {
        if(value)
            DamageEnemyAndSelf(transformToFocus);
    }

    void DamageEnemyAndSelf(Transform target)
    {
        if (canDamage)
        {
            target.GetComponent<Health>().currentHealth -= damage;
            _health.currentHealth -= selfDamage;
            StartCoroutine(CanDamageCoolDown());
        }
    }

    IEnumerator CanDamageCoolDown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageDelay);
        canDamage = true;
    }

}
