using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class assistantskill : NetworkBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem healthup;
    public ParticleSystem shieldup;
    public const int maxhealth=100;
    public int currenthealth=maxhealth;
    public int shield = 0;
    public int tempblood = maxhealth;
    private float timer_f = 0f;
    private bool  shield_flag = false; 
    private int timer_i = 0;
    void Start(){
        tempblood = gameObject.GetComponent<playerhealth>().currenthealth;
    }
    [Command]
    public void Cmdaddhealth(int amount){
        Rpchealthup();
        tempblood += 30;
        if(tempblood > maxhealth)
            tempblood = maxhealth;
        gameObject.GetComponent<playerhealth>().currenthealth = tempblood + shield;
    }
    [Command]
    public void Cmdaddshield(int amount){
        Rpcshieldup();
        shield_flag = true;
        timer_f = 0f;
        shield = 30;
        gameObject.GetComponent<playerhealth>().currenthealth = tempblood + shield;
    }
    [ClientRpc]
    public void Rpchealthup(){
        healthup.Play();
    }
    [ClientRpc]
    public void Rpcshieldup(){
        shieldup.Play();
    }
    [ClientRpc]
    public void Rpcshieldstop(){
        shieldup.Stop();
    }
    private void Update() {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        if(shield_flag == true && timer_i == 3)
        {
            shield_flag = false;
            gameObject.GetComponent<playerhealth>().currenthealth = tempblood;
            shield = 0;
            Rpcshieldstop();
        }
    }
}
