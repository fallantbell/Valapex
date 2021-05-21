using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;
    public float switchDelay;
    public int index = 0;
    private int maxWeaponHold = 2;
    private bool isSwitching = false;
    GunSystem gunSystem;
    void Start()
    {
        // if(isLocalPlayer)
        //     return;
        
        gunSystem = GetComponent<GunSystem>();
        // weapons = GetComponentsInChildren<GameObject>();
        // Debug.Log(weapons.Length);
        InitializeWeapons();
        gunSystem.InitializeWeapons();
    }
    private void InitializeWeapons(){
        for(int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);
        
        weapons[0].SetActive(true);
        // NetworkServer.Spawn(weapons[0]);
    }
    
    private void SwitchWeapons(int index){
        for(int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);

        if(index < 0)
            index = maxWeaponHold - 1;

        
        weapons[index].SetActive(true);
        // NetworkServer.Spawn(weapons[index]);
    }
    private IEnumerator SwitchAfterDelay(int index){
        isSwitching = true;
        yield return new WaitForSeconds(switchDelay);
        SwitchWeapons(index);
        
        gunSystem.InitializeWeapons();
        isSwitching = false;
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching){
            index++;
            if(index >= maxWeaponHold)
                index = 0;
            StartCoroutine(SwitchAfterDelay(index));
            Debug.Log(index);
        }   
    }
}
