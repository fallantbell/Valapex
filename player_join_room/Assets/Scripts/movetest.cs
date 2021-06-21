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
        // GameObject g=GameObject.Find("playerinfoobject");
        // if(g.GetComponent<savename>().character=="hunter"){
        //     anim=gameObject.transform.GetChild(5).gameObject.GetComponent<Animator>();
        // }
        // else if(g.GetComponent<savename>().character=="wizzard"){
        //     anim=gameObject.transform.GetChild(6).gameObject.GetComponent<Animator>();
        // }
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
