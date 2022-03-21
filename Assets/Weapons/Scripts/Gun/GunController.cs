using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    #region Variables

    [Header("Camera")]

    [SerializeField] private Transform _playerCamera;

    private GunControls _controls;

    private Animator _animator;

    private int _fireHash = Animator.StringToHash("isFire");
    private int _reloadHash = Animator.StringToHash("isReload");

    [Header("Ammo")]

    [SerializeField] private int _maxAmmo = 40;
    private int _currentAmmo;

    [Header("Fire")]

    [SerializeField] private Transform _barrel;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private TrailRenderer _hotTrail;
    [SerializeField] private ParticleSystem _metalImpact;
    [SerializeField] private ParticleSystem _fleshImpact;

    private bool _isFire;
    [SerializeField] private float _fireRate;
    private float _lastFired;

    [Header("Reload")]

    private bool _isReload;
    [SerializeField] private float _reloadTime;

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

        _controls.Gun.Reload.performed += ctx => ProcessReload();

        _controls.Gun.Fire.performed += ctx => _isFire = true;
        _controls.Gun.Fire.canceled += ctx => _isFire = false;

    }

    private void OnEnable()
    {
        _controls.Gun.Enable();
        _isReload = false;
        _animator.SetBool(_reloadHash, false);

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
        if (_isReload)
            return;

        if (Time.time - _lastFired >= 1/_fireRate)
        {
            _animator.SetBool(_fireHash, false);

            if (_currentAmmo == 0)
                return;

            if (_isFire)
            {
                Fire();
                _lastFired = Time.time;
                _currentAmmo -= 1;
                Instantiate(_muzzleFlash, _barrel.position, Quaternion.identity);
                _animator.SetBool(_fireHash, true);
            }
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit))
        {
            FireEffects(hit);
            transform.GetComponent<Damage>().InflictDamage(hit.collider.gameObject);           
        }

    }

    private void FireEffects(RaycastHit hit)
    {
        TrailRenderer trail = Instantiate(_hotTrail, _barrel.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit.point));
        if (hit.transform.CompareTag("Enemy"))
            Instantiate(_fleshImpact, hit.point, Quaternion.LookRotation(hit.normal));
        else
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
    
    private void ProcessReload()
    {
        if (_currentAmmo == _maxAmmo)
            return;

        StartCoroutine(Reload());
    
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        _animator.SetBool(_reloadHash, true);

        yield return new WaitForSeconds(_reloadTime - .25f);
        _animator.SetBool(_reloadHash, false);
        yield return new WaitForSeconds(.25f);

        _currentAmmo = _maxAmmo;
        _isReload = false;
    }

    //private voi


    #endregion

}


