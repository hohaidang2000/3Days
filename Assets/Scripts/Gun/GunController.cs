using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform playerCamera;

    private GunControls controls;

    private Animator animator;

    private int firingHash = Animator.StringToHash("firing");
    private int reloadingHash = Animator.StringToHash("reloading");

    [SerializeField] private int maxAmmo = 40;
    private int currentAmmo;
    [SerializeField] private float damage;

    [SerializeField] private Transform barrel;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private TrailRenderer hotTrail;
    
    [SerializeField] private ParticleSystem metalImpact;
    
    private bool firing;
    private float fireRate = 10f;
    private float lastFired;

    private bool reloading;
    private float reloadTime = 1.833f;

    #endregion


    #region MonoBehaviour

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        controls = new GunControls();

        controls.Gun.Reload.performed += ctx => Reload();

        controls.Gun.Fire.performed += ctx => firing = true;
        controls.Gun.Fire.canceled += ctx => firing = false;

    }

    private void OnEnable()
    {
        controls.Gun.Enable();
    }

    private void OnDisable()
    {
        controls.Gun.Disable();
    }

    private void Update()
    {
        ProcessFire();
    }

    private void FixedUpdate()
    {

    }

    #endregion


    #region Methods

    private void ProcessFire()
    {
        if (reloading)
            return;

        if (Time.time - lastFired >= 1/fireRate)
        {
            animator.SetBool(firingHash, false);

            if (currentAmmo == 0)
                return;

            if (firing)
            {
                Fire();
                lastFired = Time.time;
                currentAmmo -= 1;
                Instantiate(muzzleFlash, barrel.position, Quaternion.Euler(-transform.forward));
                animator.SetBool(firingHash, true);
            }
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit))
        {
            BulletAnimation(hit);
            transform.GetComponent<Damage>().InflictDamage(hit.collider.gameObject);           
        }

    }

    //private void InflictDamage(RaycastHit hit)
    //{
    //    hit.collider
    //    Health health = hit.transform.GetComponent<Health>();
    //    if (health != null)
    //    {
    //        health.TakeDamage(damage);
    //    }
    //}

    private void BulletAnimation(RaycastHit hit)
    {
        TrailRenderer trail = Instantiate(hotTrail, barrel.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit.point));
        Instantiate(metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hitPoint, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hitPoint;

        Destroy(trail.gameObject, trail.time);

    }
    
    private void Reload()
    {
        StartCoroutine(ReloadAnimation());
    }

    private IEnumerator ReloadAnimation()
    {
        reloading = true;
        animator.SetBool(reloadingHash, true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool(reloadingHash, false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        reloading = false;
    }

    //private voi


    #endregion

}


