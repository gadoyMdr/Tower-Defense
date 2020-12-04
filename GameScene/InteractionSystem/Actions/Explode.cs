using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explode : MonoBehaviour, IActionable
{
    [SerializeField]
    [Range(0.01f, 5)]
    private float delay = 2;

    [SerializeField]
    [Range(0.01f, 200)]
    private float explosionDamage;

    [SerializeField]
    [Range(0.01f, 20)]
    private float explosionRadius;

    [SerializeField]
    private ParticleSystem explosionParticles;

    public void TriggerAction(Transform transformToFocus, bool value)
    {
        if(value)
            StartCoroutine(ExplodeMethod());
    }


    IEnumerator ExplodeMethod()
    {
        yield return new WaitForSeconds(delay);

        Physics.OverlapSphere(transform.position, explosionRadius)
            .Select(x => x.GetComponent<Health>()).OfType<Health>()
            .ToList()
            .ForEach(x => x.currentHealth -= (1 - Vector3.Distance(x.transform.position, transform.position) / explosionRadius) * explosionDamage);

        ParticleSystem newExplosion = Instantiate(explosionParticles, transform.position, transform.rotation, null);
        newExplosion.Play();

        Destroy(gameObject);
    }
}
