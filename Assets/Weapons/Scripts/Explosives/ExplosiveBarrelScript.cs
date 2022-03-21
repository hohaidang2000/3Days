using UnityEngine;
using System.Collections;

public class ExplosiveBarrelScript : MonoBehaviour
{

    private float _randomTime;

    [Header("Prefabs")]
    //The explosion prefab
    public Transform explosion;
    //The destroyed barrel prefab
    public Transform destroyedBarrel;

    [Header("Customizable Options")]
    //Minimum time before the barrel explodes
    public float minTime = 0.05f;
    //Maximum time before the barrel explodes
    public float maxTime = 0.25f;

    [Header("Explosion Options")]
    //How far the explosion will reach
    public float explosionRadius = 12.5f;
    //How powerful the explosion is
    public float explosionForce = 4000.0f;

    public void StartExplode()
    {
        _randomTime = Random.Range(minTime, maxTime);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //Wait for set amount of time
        yield return new WaitForSeconds(_randomTime);

        //Spawn the destroyed barrel prefab
        Instantiate(destroyedBarrel, transform.position, transform.rotation);

        //Explosion force
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rb))
                rb.AddExplosionForce(explosionForce * 50, explosionPos, explosionRadius);

            transform.GetComponent<Damage>().InflictDamage(hit.gameObject);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}