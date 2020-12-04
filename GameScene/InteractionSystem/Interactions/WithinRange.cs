using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WithinRange : MonoBehaviour, IInteractable
{
    public Action<Transform, bool> TransformFocusedAction { get; set; }
    public bool isActivated { get; set; }

    [SerializeField]
    [Range(0.1f, 100)]
    private float rangeRadius;

    [SerializeField]
    private bool justWithinRange = false;

    [SerializeField]
    private BaseClassOrderBy orderBy;

    void Start()
    {
        isActivated = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, rangeRadius);
    }


    private void Update()
    {
        if(isActivated)
            CheckEnemiesAround();
    }

    void CheckEnemiesAround()
    {
        //Get all enemies within range
        List<Transform> enemiesToShoot = new List<Transform>(Physics.OverlapSphere(transform.position, rangeRadius)?
            .Where(x => x.CompareTag("Enemy") && x.GetComponent<Health>().currentHealth > 0)?
            .Select(x => x.transform));

        if (justWithinRange)
        {
            if (enemiesToShoot.Count > 0) TransformFocusedAction?.Invoke(null, true);
            else TransformFocusedAction?.Invoke(null, false);
        }
        else
        {

            List<Transform> visibleEnemies = new List<Transform>();
            //Check if an object is in-between
            foreach (Transform t in enemiesToShoot)
            {
                RaycastHit[] hit = Physics.RaycastAll(transform.position, (t.position - transform.position).normalized, (t.position - transform.position).magnitude);

                if (hit.Length <= 2)
                    visibleEnemies.Add(t);
            }

            Transform closestVisibleEnemy = visibleEnemies.OrderBy(x => x.GetComponent<Health>().currentHealth).FirstOrDefault();

            if (visibleEnemies.Count > 0) TransformFocusedAction?.Invoke(closestVisibleEnemy, true);
            else TransformFocusedAction?.Invoke(closestVisibleEnemy, false);
        }

        
    }
}
