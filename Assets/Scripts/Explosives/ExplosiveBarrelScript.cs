using UnityEngine;
using System.Collections;

public class ExplosiveBarrelScript : MonoBehaviour
{

    private float randomTime;

    //Used to check if the barrel 
    //has been hit and should explode 
    public bool explode = false;

    [Header("Prefabs")]
    //The explosion prefab
    public Transform explosionPrefab;
    //The destroyed barrel prefab
    public Transform destroyedBarrelPrefab;

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

    public void ExplodeProcess()
    {
        randomTime = Random.Range(minTime, maxTime);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //Wait for set amount of time
        yield return new WaitForSeconds(randomTime);

        //Spawn the destroyed barrel prefab
        Instantiate(destroyedBarrelPrefab, transform.position,transform.rotation);

        //Explosion force
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            
            if (hit.TryGetComponent(out Rigidbody rb))
                rb.AddExplosionForce(explosionForce * 50, explosionPos, explosionRadius);

            transform.GetComponent<Damage>().InflictDamage(hit.gameObject);

        }

        //Raycast downwards to check the ground tag
        RaycastHit checkGround;
        if (Physics.Raycast(transform.position, Vector3.down, out checkGround, 50))
        {
            Instantiate(explosionPrefab, checkGround.point,
                Quaternion.FromToRotation(Vector3.forward, checkGround.normal));
        }

        //Destroy the current barrel object
        Destroy(gameObject);
    }
}