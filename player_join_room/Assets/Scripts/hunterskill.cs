using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Mirror;

public class hunterskill : NetworkBehaviour
{
    // Start is called before the first frame update

    private Transform playerCamera;
    private GameObject m_lightObj;
    private Light m_light;
    private float tf = 0;
    private int ti = 0;
    private bool flag = false;
    public Color endcolor;
    private bool lightchange = false;
    public ParticleSystem stock;
    private FirstPersonController fpsController;

	// Use this for initialization
	//初始化，找到名為FirstPersonCharacter的物件
	void Start ()
	{
        m_lightObj = GameObject.Find("Directional Light");
        //获取灯光游戏对象的Light组件
        m_light = m_lightObj.GetComponent<Light>();
		playerCamera = transform.Find("FirstPersonCharacter");
	}
    void Update()
    {
        if(flag == true)
        {
            tf += Time.deltaTime;
            ti = (int)tf;
            if(ti == 5)
            {
                m_light.color = new Color(1f, 1f, 1f);
                gameObject.GetComponent<changeMaterial>().enabled=false;
                flag = false;               
            }
        }
    }
    // Update is called once per frame
    [Command]
    public void Cmdsettrap()
    {
        if(isLocalPlayer)
		{
			GameObject instance = Instantiate (Resources.Load ("trap")) as GameObject;
            Vector3 move = gameObject.transform.position;
            move = new Vector3(move.x-1, move.y, move.z );
            // instance.transform.position = playerCamera.position + playerCamera.forward * -1.5f + playerCamera.up * -1.5f;
            instance.transform.position = move;
            NetworkServer.Spawn (instance);
            
		}
    }
    [Command]
    public void Cmdstock()
    {
        Rpcstock();
    }
    [ClientRpc]
    public void Rpcstock()
    {
        stock.Play();
    }
    [ClientRpc]
    public void Rpcgetintotrap()
    {
        if(!isLocalPlayer) return;
        CharacterController cc = GetComponent(typeof(CharacterController)) as CharacterController;
        cc.enabled = false;
    }
    [ClientRpc]
    public void Rpcgetoutofftrap()
    {
        if(!isLocalPlayer) return;
        CharacterController cc = GetComponent(typeof(CharacterController)) as CharacterController;
        cc.enabled = true;
    }
    public void changelight()
    {
        flag = true;
        tf = 0;
        m_light.color = new Color(0f, 0.005f, 0.6f);
    }

}
