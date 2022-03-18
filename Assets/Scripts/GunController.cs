using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    #region Variables

    [SerializeField] private int maxAmmo = 30;
    private int minAmmo;
    private int currentAmmo;

    [SerializeField] private Transform cam;

    private InputControls controls;

    private Animator animator;

    private int firingHash = Animator.StringToHash("firing");
    private int reloadingHash = Animator.StringToHash("reloading");

    private bool firing;
    private float fireRate = 10f;
    private float lastFired;

    [SerializeField] private Transform barrel;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem metalImpact;
    [SerializeField] private TrailRenderer hotTrail;

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

        controls = new InputControls();

        controls.Gun.Reload.performed += ctx => Reload();


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
        ProcessInputs();
        FireProcess();
    }

    private void FixedUpdate()
    {

    }

    #endregion


    #region Methods

    private void ProcessInputs()
    {

    }


    private void FireProcess()
    {
        if (reloading)
            return;

        if (Time.time - lastFired >= 1/fireRate)
        {

            firing = controls.Gun.Fire.ReadValue<float>() > 0;

            animator.SetBool(firingHash, false);
            
            if (firing)
            {
                lastFired = Time.time;
                Fire();
                Instantiate(muzzleFlash, barrel.position, Quaternion.Euler(0f, 180f, 0f));
                animator.SetBool(firingHash, true);
            }
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit))
        {
            TrailRenderer trail = Instantiate(hotTrail, barrel.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit.point));
            Instantiate(metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
        }

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


    #endregion

}
