using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MyObjectToPlace
{
    public System.Action<Vector3, Vector3> FocusNewTarget;

    [SerializeField]
    [Range(0.01f, 10f)]
    private float rateOfFire = 1f;

    [SerializeField]
    [Range(0.01f, 200f)]
    private float muzzleVelocity = 1f;

    [SerializeField]
    [Range(1f, 200f)]
    private float damage = 25f;

    [SerializeField]
    [Range(1, 10)]
    private int bulletsPerShot = 1;

    [SerializeField]
    [Range(0f, 20f)]
    private float accuracy = 0f;

    [SerializeField]
    private List<Transform> muzzlePoints = new List<Transform>();

    public Transform gunRotate;

    [SerializeField]
    private Bullet bullet;

    [HideInInspector]
    public Vector3 averageMuzzlePointsPos;
    bool canFire = true;

    private void Start()
    {
        Randomize();
        GetAverageMuzzlePoint();
    }

    private void Update()
    {
        FocusNewTarget?.Invoke(
            muzzlePoints[0].transform.position, 
            muzzlePoints[0].transform.position + muzzlePoints[0].transform.forward * 150
        );
    }

    public void GetAverageMuzzlePoint()
    {
        averageMuzzlePointsPos = new Vector3
            (
                muzzlePoints.Sum(x => x.position.x),
                muzzlePoints.Sum(x => x.position.y),
                muzzlePoints.Sum(x => x.position.z)
            ) / muzzlePoints.Count;
    }

    Vector3 GetAccuracy(Transform muzzlePoint) => (muzzlePoint.position + muzzlePoint.forward * 10) + Random.insideUnitSphere * accuracy;

    public void TryFire()
    {
        if (!canFire) return;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            muzzlePoints.ForEach(x => Instantiate(bullet, x.position, Quaternion.LookRotation((GetAccuracy(x) - x.position).normalized), null).Trigger(muzzleVelocity, damage));
        }
        
        
        StartCoroutine(CoolDownCoroutine());
    }

    IEnumerator CoolDownCoroutine()
    {
        canFire = false;
        yield return new WaitForSeconds(rateOfFire);
        canFire = true;
    }

    public override bool CanBePlaced()
    {
        return _collidersInTrigger.colliders.Count == 0 || _collidersInTrigger.colliders.Count == 1 && _collidersInTrigger.colliders.Any(x => x.gameObject.name.Contains("Warning"));
    }

    void Randomize()
    {
        rateOfFire *= Random.Range(0.8f, 1.2f);
    }
}
