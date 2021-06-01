using UnityEngine;
using System.Collections;
using Mirror;

public class CannonballController : NetworkBehaviour
{
	private float age;
	public float maxAge = 1.0f;

	private int bulletdamage; //子彈傷害

	// Use this for initialization
	//初始化
	void Start ()
	{
		//初始子彈傷害
		if(gameObject.name=="Rifle"){      //   <==================根據不同槍起始不同子彈傷害
			bulletdamage=34;
		}
		else if(gameObject.name=="AssaultRifle"){
			bulletdamage=15;
		}



		age = 0.0f;	
	}

	void OnCollisionEnter(Collision other)
	{
		if(!isServer) return;
		Debug.Log("enter"+other.gameObject.name);
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<playerhealth>().takedamage(bulletdamage); //  <================打到人 扣血
		}
		NetworkServer.Destroy(gameObject);
	}

	// Update is called once per frame
	// 每顆球的生命週期為maxAge秒，超過就刪除
	[ServerCallback]
	void Update () 
	{	
		age += Time.deltaTime;
		if( age > maxAge )
		{	
			NetworkServer.Destroy(gameObject);
		}
	}
}
