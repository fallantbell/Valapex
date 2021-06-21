using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
public class player_clone_control : NetworkBehaviour
{
    public GameObject floatinginfo;
    public GameObject healthbar;
    public TextMesh nametext;
    public RectTransform heal;

    public Vector3 rot;
    private float speed=5.0f;
    private float age;
    private float maxage=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        // 因為只有server的資訊正確
        // 所以另外做clientrpc同步名稱 血量
        if(isServer){ 
            Rpcsyncinfo(nametext.text,heal.sizeDelta);
        }
        age=0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject me=GameObject.Find("ME");
        floatinginfo.transform.LookAt(me.transform);
        healthbar.transform.LookAt(me.transform);

        Vector3 move = transform.forward ;

        gameObject.transform.position+=move * speed * Time.deltaTime;
        // Debug.Log("rot:"+rot);
        age+=Time.deltaTime;
        if(age>maxage){
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    public void Rpcsyncinfo(string tex,Vector2 v2){
        nametext.text=tex;
        heal.sizeDelta=v2;
    }
}
