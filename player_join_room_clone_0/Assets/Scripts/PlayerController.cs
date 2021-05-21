using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Mirror;

public class PlayerController: NetworkBehaviour 
{
	private FirstPersonController fpsController;
	private Transform playerCameraTransform;
	private Camera playerCamera;
	private MouseLook mouseLook;
	private GunSystem gunSystem;
	private WeaponManager weaponManager;
	private AudioListener playerAudioListener;

	private GameObject[] gameObjects;

	void Start()
	{
		if(isLocalPlayer){
			gameObject.name = "ME";
		}
		else{
			//當角色被產生出來時，如果不是Local Player就把所有的控制項目關閉，這些角色的位置資料將由Server來同步
			
			fpsController = GetComponent<FirstPersonController>();
			// playerCameraTransform = transform.Find("FirstPersonCharacter");
			// playerAudioListener = playerCameraTransform.GetComponent<AudioListener>();
			// ------------------------------------------------------------------------------------------------------------------------
			gunSystem = GetComponent<GunSystem>();
			weaponManager = GetComponent<WeaponManager>();
			playerCamera = transform.Find("CameraMouseLook").GetComponentInChildren<Camera>();
			mouseLook = playerCamera.GetComponentInChildren<MouseLook>();
			
			Debug.Log("Camera Name" + playerCamera.name);
			
			
			if(mouseLook)
				mouseLook.enabled = false;

			// if(weaponManager){
			// 	weaponManager.enabled = false;
			// }
				

			// if(gunSystem)
			// 	gunSystem.enabled = false;

			
			// foreach(GunSystem gun in gunSystems){
			// 	if(gun)
			// 		gun.enabled = false;
			// }
			// ------------------------------------------------------------------------------------------------------------------------
			if (fpsController) {
				fpsController.enabled = false;
			}
			if(playerCamera)
			{
				playerCamera.enabled = false;
			}
			// if(playerAudioListener)
			// {
			// 	playerAudioListener.enabled = false;
			// }
		}
		GameObject g=GameObject.Find("allplayer");
		g.GetComponent<allplayer>().allplayerlist.Add(gameObject);
	}
}

