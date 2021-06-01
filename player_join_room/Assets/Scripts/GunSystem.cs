using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class GunSystem : NetworkBehaviour
{
    //Gun stats
    public int magazineSize, bulletsPerTap;
    public float fireRate, spread, reloadTime, timeBetweenShots;
    public bool allowHold;
    public int bulletLeft, bulletsShot;

    public bool shooting, reloading, readyToShoot;

    public Camera fpsCam;
    public GameObject bulletHole;
    public ParticleSystem bulletTrail, muzzleFlash;
    // public CamShake camShake;
    public Weapon_Recoil recoil;
    public CamRecoil camRecoil;
    public LayerMask objectShot;
    public AudioSource gunShots;
    public float recoilAmount;
    GameObject gunHolder, gunType;
    GunStats gunStats;
    WeaponManager weaponManager;
    public float power;

    private void GetInput(){
        if(allowHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //reload
        if(Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading){
            Reload();
        }
        if(shooting && bulletLeft <= 0)
            Reload();
        //shoot
        if(readyToShoot && shooting && !reloading && bulletLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        
    }
<<<<<<< Updated upstream
    [Command]
    void CmdShootingAnimation(){
        RpcShootingAnimation();
    }
    [ClientRpc]
    void RpcShootingAnimation(){
        ShootingAnimation();
    }
    private void ShootingAnimation(){
=======
    private IEnumerator ShootingAnimation(){
>>>>>>> Stashed changes
        shooting = true;
        muzzleFlash.Play();
        bulletTrail.Play();
        gunShots.Play();
<<<<<<< Updated upstream
=======
        yield return null;

>>>>>>> Stashed changes
    }
    [Command]
    public void CmdSpawnBullet () 
    {
        
        //we instantiate one from Resources
        GameObject instance = Instantiate (Resources.Load ("bullet")) as GameObject;
        //Let's name it
        Debug.Log(instance.name);
        instance.name = "AssaultRifle"; // <=======給與子彈名稱 之後根據名稱決定傷害
        
        // Debug.Log(gunType.name);

        //Let's position it at the player
<<<<<<< Updated upstream
        instance.transform.localRotation = Quaternion.Euler(fpsCam.transform.forward);
=======
>>>>>>> Stashed changes
        instance.transform.position = gunType.transform.Find("AttackPoint").position;
        instance.GetComponent<Rigidbody> ().AddForce (fpsCam.transform.forward * power);
        //
        
        NetworkServer.Spawn (instance);
    }
    
    private void Shoot(){
<<<<<<< Updated upstream
        
        if(!isLocalPlayer){
            return;
        }
=======
        if(!isLocalPlayer) return;
>>>>>>> Stashed changes

        readyToShoot = false;
        
        //spread
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);
        
        //Calculate direction with spread
        Vector3 spreadDirection = fpsCam.transform.forward + new Vector3(xSpread, ySpread, 0);
        
<<<<<<< Updated upstream
=======
        Debug.Log("shoot");
>>>>>>> Stashed changes
        //RayCast
        // if(Physics.Raycast(fpsCam.transform.position, spreadDirection, out hit, range)){
        //     Debug.Log(hit.collider.name);

        //     if(hit.collider.CompareTag("Player")){
        //         hit.collider.GetComponent<playerhealth>().takedamage(dmg);
        //     }
        // }
        
        recoil.Fire();
        camRecoil.Fire();
<<<<<<< Updated upstream

        CmdShootingAnimation();
=======
        StartCoroutine(ShootingAnimation());

>>>>>>> Stashed changes
        CmdSpawnBullet();

        bulletLeft -= 1;
        bulletsShot -= 1;
        Invoke("ResetShot", fireRate);
        
        // if(bulletLeft > 0 && bulletsShot > 0)
        //     Invoke("Shoot", timeBetweenShots);
    }
    
    private void ResetShot(){
        readyToShoot = true;
    }
    private void Reload(){
        reloading = true;
        
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished(){
        bulletLeft = magazineSize;
        reloading = false;
    }
        
    void Start()
    {
        readyToShoot = true;
    }
    void GunSystemInitialize(){
        gunHolder=gameObject.transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).gameObject;
        // gunHolder = GameObject.Find("CameraMouseLook/MainCamera/GunHolder");
        Debug.Log(gunHolder.name);
<<<<<<< Updated upstream
        weaponManager = gameObject.GetComponent<WeaponManager>();
        fpsCam = gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<Camera>();
        camRecoil = gameObject.transform.Find("CameraMouseLook").gameObject.GetComponent<CamRecoil>();
=======
        weaponManager = GetComponent<WeaponManager>();
        fpsCam = transform.Find("CameraMouseLook").GetComponentInChildren<Camera>();
        camRecoil = transform.Find("CameraMouseLook").GetComponent<CamRecoil>();
>>>>>>> Stashed changes
    }
    public void InitializeWeapons(){
        gunType = gunHolder.transform.GetChild(weaponManager.index).gameObject;
        Debug.Log("gun playername:"+gameObject.name);
        Debug.Log("Gun : " + gunType.name);
        Debug.Log("Available guns : "+ gunHolder.transform.childCount);
        gunShots = gunType.GetComponent<AudioSource>();
        recoil = gunType.GetComponent<Weapon_Recoil>();
        
        muzzleFlash = gunType.transform.Find("AttackPoint/MuzzleFlashEffect").GetComponent<ParticleSystem>();
        bulletTrail = gunType.transform.Find("AttackPoint/BulletTrail").GetComponent<ParticleSystem>();
        
        gunStats = gunType.GetComponent<GunStats>();
        magazineSize = gunStats.magazineSize;
        bulletsPerTap = gunStats.bulletsPerTap;
        fireRate = gunStats.fireRate;
        spread = gunStats.spread;
        reloadTime = gunStats.reloadTime;
        timeBetweenShots = gunStats.timeBetweenShots;
        allowHold = gunStats.allowHold;
        bulletLeft = gunStats.bulletLeft;
        recoilAmount = gunStats.recoilAmount;
        reloading = gunStats.reloading;
        power = gunStats.power;
    }
    void Awake(){
        // if(isLocalPlayer) 
        //     return;
        GunSystemInitialize();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();  
    }
}
