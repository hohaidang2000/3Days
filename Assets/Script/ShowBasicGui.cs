using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowBasicGui : MonoBehaviour
{
    public GunController gunController;
    public TextMeshProUGUI health;
    public TextMeshProUGUI bullet;
    public Health healthScript;
    public float healthpoint;
    public int bulletpoint;
    public int meazine;
    // Start is called before the first frame update
    void Start()
    {
        healthpoint = healthScript._health;
        meazine = gunController._maxAmmo ;
        bulletpoint = gunController._currentAmmo;

}

    // Update is called once per frame
    void Update()
    {
        healthpoint = healthScript._health;
        meazine = gunController._maxAmmo;
        bulletpoint = gunController._currentAmmo;
        health.SetText(healthpoint.ToString());
        bullet.SetText(bulletpoint.ToString() + " / " + meazine.ToString());
    }
}
