using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private Transform currentWeapon;
    [SerializeField] private Transform aiming;
    [SerializeField] private Transform normal;
    [SerializeField] private Transform switchPosition;
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject pistol;
    private float aimSpeed = 10f;
    private float switchSpeed = 6f;
    private bool startAim;
    private bool switchWeaponFinish;
    private int switchWeapon;
    private float mouseScroll;
    private float startTimeSwitch;
    private void OnEnable()
    {
        controls.Weapon.Enable();
    }
    private void OnDisable()
    {
        controls.Weapon.Disable();
    }
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Weapon.Aim.performed += ctx => startAim = true;
        controls.Weapon.Aim.canceled += ctx => startAim = false;

        controls.Weapon.SwitchWeapon.performed += ctx => mouseScroll = ctx.ReadValue<float>();
    }
    void Start()
    {   
        switchWeapon=0;
        mouseScroll=0;
        startAim=false;
        switchWeaponFinish = true;
        rifle.SetActive(true);
        pistol.SetActive(false);
    }
    void Update()
    {  
        Aim();
        SwitchWeapon();
    }
    void Aim(){
        if (!switchWeaponFinish) return;

        if (startAim) 
            currentWeapon.transform.position = Vector3.Lerp(currentWeapon.transform.position, aiming.position, Time.deltaTime*aimSpeed);
        else
            currentWeapon.transform.position = Vector3.Lerp(currentWeapon.transform.position, normal.position, Time.deltaTime*aimSpeed);
    }
    void SwitchWeapon(){
        if (mouseScroll == 0)
        {
            if (Time.time - startTimeSwitch > 0.5f && !switchWeaponFinish)
            {
                switchWeaponFinish = true;
            }
            else if (!switchWeaponFinish)
            {
                currentWeapon.transform.position = Vector3.Lerp(currentWeapon.transform.position, normal.position, Time.deltaTime*switchSpeed);
            }
            
        }
        if (mouseScroll != 0)
        {
            switchWeapon= 1 - switchWeapon;
            currentWeapon.transform.position = switchPosition.position;
            switchWeaponFinish = false;
            startTimeSwitch=Time.time;
        }
        if (switchWeapon == 0) 
        {
            rifle.SetActive(true);
            pistol.SetActive(false);
        }
        else 
        {
            rifle.SetActive(false);
            pistol.SetActive(true);
        }
    }
}
