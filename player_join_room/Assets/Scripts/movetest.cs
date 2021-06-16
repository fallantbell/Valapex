using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class movetest : NetworkBehaviour
{
    public Animator anim;
    public int state=0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer) return;
        if(Input.GetKey(KeyCode.W)){
            state=1;
        }
        else {
            state=0;
        }
        anim.SetInteger("run",state);
    }
}
