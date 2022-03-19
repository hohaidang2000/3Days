using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform _playerCamera;

    private GunControls _controls;

    private Animator _animator;

    private int _firingHash = Animator.StringToHash("firing");
    private int _reloadingHash = Animator.StringToHash("reloading");

    [SerializeField] private int _maxAmmo = 40;
    private int _currentAmmo;

    [SerializeField] private Transform _barrel;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private TrailRenderer _hotTrail;
    
    [SerializeField] private ParticleSystem _metalImpact;
    
    private bool _firing;
    private float _fireRate = 10f;
    private float _lastFired;

    private bool _reloading;
    private float _reloadTime = 1.833f;

    #endregion


    #region MonoBehaviour

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _controls = new GunControls();

        _controls.Gun.Reload.performed += ctx => Reload();

        _controls.Gun.Fire.performed += ctx => _firing = true;
        _controls.Gun.Fire.canceled += ctx => _firing = false;

    }

    private void OnEnable()
    {
        _controls.Gun.Enable();
    }

    private void OnDisable()
    {
        _controls.Gun.Disable();
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
        if (_reloading)
            return;

        if (Time.time - _lastFired >= 1/_fireRate)
        {
            _animator.SetBool(_firingHash, false);

            if (_currentAmmo == 0)
                return;

            if (_firing)
            {
                Fire();
                _lastFired = Time.time;
                _currentAmmo -= 1;
                Instantiate(_muzzleFlash, _barrel.position, Quaternion.Euler(-transform.forward));
                _animator.SetBool(_firingHash, true);
            }
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit))
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
        TrailRenderer trail = Instantiate(_hotTrail, _barrel.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit.point));
        Instantiate(_metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
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
        _reloading = true;
        _animator.SetBool(_reloadingHash, true);

        yield return new WaitForSeconds(_reloadTime - .25f);
        _animator.SetBool(_reloadingHash, false);
        yield return new WaitForSeconds(.25f);

        _currentAmmo = _maxAmmo;
        _reloading = false;
    }

    //private voi


    #endregion

}


