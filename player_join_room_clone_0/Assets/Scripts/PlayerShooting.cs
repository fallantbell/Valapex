using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun;

    public GameObject bullet;

    public float bulletSpeed;

    //Gun stats
    public int dmg;
    public float fireRate, spread, range, reloadTime;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //Gun phase
    bool shooting, readyToShoot, reloading;

    //Reference to other objects
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask hitTarget;

    void Start()
    {
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(bullet.gameObject, gun.transform.position, gun.transform.rotation).GetComponent<Bullet>();  
    }
}
