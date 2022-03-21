using UnityEngine;
using System.Collections;

public class GasTankScript : MonoBehaviour
{

    #region Variables

    [Header("Prefabs")]
    //Explosion prefab
    public Transform explosion;
    //The destroyed gas tank prefab
    public Transform destroyedGasTank;

    [Header("Explosion Options")]
    //How far the explosion will reach
    public float explosionRadius = 12.5f;
    //How powerful the explosion is
    public float explosionForce = 4000.0f;

    [Header("Audio")]
    public AudioSource impactSound;

    #endregion


    #region Methods
    public void StartExplode()
    {
        StartCoroutine(Explode());
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Play the impact sound on every collision
        impactSound.Play();
    }

    private IEnumerator Explode()
    {
        //Wait for set amount of time
        yield return new WaitForSeconds(0.05f);

        //Spawn the destroyed gas tank prefab
        Instantiate(destroyedGasTank, transform.position, transform.rotation);

        //Explosion force
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rb))
                rb.AddExplosionForce(explosionForce * 50, explosionPos, explosionRadius);

            transform.GetComponent<Damage>().InflictDamage(hit.gameObject);
        }

        //Spawn the explosion prefab
        Instantiate(explosion, transform.position, transform.rotation);

        //Destroy the current gas tank object
        Destroy(gameObject);
    }

    #endregion


}