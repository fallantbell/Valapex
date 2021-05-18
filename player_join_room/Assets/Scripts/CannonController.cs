using UnityEngine;
using System.Collections;
using Mirror;

public class CannonController : NetworkBehaviour
{
	private float power;
	private Transform playerCamera;

	// Use this for initialization
	//初始化，找到名為FirstPersonCharacter的物件
	void Start ()
	{
		power = 1200.0f; // 子彈飛行速度
		playerCamera = transform.Find("FirstPersonCharacter");
	}
	
	// Update is called once per frame
	//如果是本地玩家，才能觸發射擊
	void Update ()
	{
		if(isLocalPlayer)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				CmdSpawnCannonball();
			}
		}
	}

	//告訴Server射擊，這樣球才能同步到所有的Client
	[Command]
	void CmdSpawnCannonball ()
	{
		//we instantiate one from Resources
		GameObject instance = Instantiate (Resources.Load ("bullet")) as GameObject;
		//Let's name it
		instance.name = "sniper_bullet"; // <=======給與子彈名稱 之後根據名稱決定傷害


		//Let's position it at the player
		instance.transform.position = playerCamera.position + playerCamera.forward * 1.5f + playerCamera.up * -.5f;
		instance.GetComponent<Rigidbody> ().AddForce (playerCamera.forward * power);
		//
		NetworkServer.Spawn (instance);
	}
}
