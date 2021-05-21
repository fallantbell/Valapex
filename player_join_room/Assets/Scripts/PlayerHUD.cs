using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text ui;
    GunSystem gunStats;
    int ammo;
    void Start()
    {
        ui = GetComponentInChildren<TMP_Text>();
        gunStats = GetComponentInChildren<GunSystem>();
    }

    // Update is called once per frame
    private IEnumerator WaitForSwitch(){
        yield return new WaitForSeconds(0.5f);
        gunStats = GetComponentInChildren<GunSystem>();
    }
    void Update()
    {
        if(gunStats.magazineSize == 0)
            return;
            
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
            StartCoroutine(WaitForSwitch());
        if(gunStats.reloading)
            ui.SetText("Reloading...");
        else{
            ammo = gunStats.bulletLeft / gunStats.bulletsPerTap;
            ui.SetText(ammo.ToString() + " / " + gunStats.magazineSize.ToString());
        }
    }
}
