using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using Mirror;

public class trapcontroller : NetworkBehaviour{
    // Start is called before the first frame update
    private int timer_i; //子彈傷害
    private float timer_f;
    private bool trap_flag = false;
    private GameObject player;

    // private FirstPersonController fpsController;


    // Update is called once per frame
    void OnCollisionEnter(Collision other)
	{
		if(!isServer) return;
		if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<hunterskill>().Rpcgetintotrap();
            other.gameObject.GetComponent<hunterskill>().Cmdstock();
            if(trap_flag == false)
            {
                trap_flag = true;
                timer_f = 0f;
            }
            player = other.gameObject;
		}
	}
    private void Update() {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        if(trap_flag == true && timer_i == 3)
        {
            Debug.Log(timer_i);
            trap_flag = false;
            player.GetComponent<hunterskill>().Rpcgetoutofftrap();
            NetworkServer.Destroy(gameObject);
        }
    }
}
