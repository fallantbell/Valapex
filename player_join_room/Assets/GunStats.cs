using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunStats : MonoBehaviour
{
    public int magazineSize, bulletsPerTap;
    public float fireRate, spread, range, reloadTime, timeBetweenShots;
    public bool allowHold;
    public int bulletLeft;
    public float recoilAmount;
    public bool reloading = false;
    public float power;
    GunSystem gunSystem;

    void Awake(){
        gunSystem = GetComponentInParent<GunSystem>();
        bulletLeft = magazineSize;
    }
    void OnDisable(){
        bulletLeft = gunSystem.bulletLeft;
        reloading = gunSystem.reloading;
    }
}
