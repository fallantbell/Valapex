using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using Mirror;

public class WeaponManager : NetworkBehaviour
=======


public class WeaponManager : MonoBehaviour
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
        // if(isLocalPlayer)
        //     return;
        
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    [Command]
    void CmdSwitchWeapon(int index){

        Debug.Log("Tell the server");
        RpcSwitchWeapon(index);
        // StartCoroutine(SwitchAfterDelay(index));
    }
    [ClientRpc]
    void RpcSwitchWeapon(int index){
        Debug.Log("Tell the clients");
        SwitchWeapons(index);
    }
    private void SwitchWeapons(int index){
        Debug.Log ("Equipping weapons for = " + this.name);

=======
    
    private void SwitchWeapons(int index){
>>>>>>> Stashed changes
        for(int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);

        if(index < 0)
            index = maxWeaponHold - 1;

        
        weapons[index].SetActive(true);
<<<<<<< Updated upstream
        gunSystem.InitializeWeapons();
=======
>>>>>>> Stashed changes
        // NetworkServer.Spawn(weapons[index]);
    }
    private IEnumerator SwitchAfterDelay(int index){
        isSwitching = true;
<<<<<<< Updated upstream

        yield return new WaitForSeconds(switchDelay);
        CmdSwitchWeapon(index);

        if(isLocalPlayer)
            SwitchWeapons(index);

        isSwitching = false;
    }
=======
        yield return new WaitForSeconds(switchDelay);
        SwitchWeapons(index);
        
        gunSystem.InitializeWeapons();
        isSwitching = false;
    }
    // Update is called once per frame
>>>>>>> Stashed changes

    void FixedUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching){
            index++;
            if(index >= maxWeaponHold)
                index = 0;
<<<<<<< Updated upstream

            StartCoroutine(SwitchAfterDelay(index));
            // isSwitching = true;
            // Invoke("SwitchAfterDelay(index)", switchDelay);
=======
            StartCoroutine(SwitchAfterDelay(index));
>>>>>>> Stashed changes
            Debug.Log(index);
        }   
    }
}
