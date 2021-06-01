using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
public class playerscript : NetworkBehaviour
{
    public GameObject floatinginfo;
    public GameObject healthbar;
    public TextMesh nametext;
    private Material playermaterialclone;
    public int playernumber=0;

    private float time_f=0;
    private int time_i=0; //用來計算準備倒數計時時間
    private int timetmp=-1;
    bool readyflag=false; //用來判斷準備開始

    [SyncVar(hook = nameof(playernamechange))]
    private string playername;

    [SyncVar(hook = nameof(colornamechange))]
    private Color playernamecolor;
<<<<<<< Updated upstream
    private FirstPersonController fpsController;
	private Transform playerCameraTransform;
	private Camera playerCamera;
	private MouseLook mouseLook;
	private GunSystem gunSystem;
	private WeaponManager weaponManager;
	private AudioListener playerAudioListener;

	private GameObject[] gameObjects;

=======
    GunSystem gunSystem;
>>>>>>> Stashed changes
    private void playernamechange(string oldstr, string newstr){
        nametext.text=newstr;
        Debug.Log(nametext.text);
    }
    private void colornamechange(Color oldcolor, Color newcolor){
        nametext.color=newcolor;
        playermaterialclone= new Material(GetComponentInChildren<Renderer>().material);
        playermaterialclone.SetColor(name:"_EmissionColor",newcolor);

        GetComponentInChildren<Renderer>().material=playermaterialclone;
    }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        if(!isLocalPlayer) {
            fpsController = GetComponent<FirstPersonController>();
            gunSystem = GetComponent<GunSystem>();
			weaponManager = GetComponent<WeaponManager>();
			playerCamera = transform.Find("CameraMouseLook").GetComponentInChildren<Camera>();
			mouseLook = playerCamera.GetComponentInChildren<MouseLook>();
            if(mouseLook)
				mouseLook.enabled = false;

            if (fpsController) {
				fpsController.enabled = false;
			}
			if(playerCamera){
				playerCamera.enabled = false;
			}
=======
        if(!isLocalPlayer) return;

        // gunSystem = gameObject.GetComponent<GunSystem>();
        // gunSystem.InitializeWeapons();

        floatinginfo.transform.localPosition=new Vector3(x:-2.29f , y:0.11f , z:3f);
        floatinginfo.transform.localScale=new Vector3(x:1f , y:1f , z:1f);

        healthbar.transform.localPosition=new Vector3(x:-2.47f , y:-0.545f , z:2.67f);
        healthbar.transform.localScale=new Vector3(x:-0.027536f , y:0.01f , z:0.01f);

        GameObject gameui=GameObject.Find("GameUI"); //隱藏起始介面
        gameui.SetActive(false);

        GameObject readyUI=GameObject.Find("readytoroom"); //啟動準備介面
        readyUI.SetActive(true);
>>>>>>> Stashed changes

        }
        else{
            gameObject.name = "ME"; //將localplayer 名稱設為ME

            // floatinginfo.transform.localPosition=new Vector3(x:-2.29f , y:0.11f , z:3f);
            // floatinginfo.transform.localScale=new Vector3(x:1f , y:1f , z:1f);

            // healthbar.transform.localPosition=new Vector3(x:-2.47f , y:-0.545f , z:2.67f);
            // healthbar.transform.localScale=new Vector3(x:-0.027536f , y:0.01f , z:0.01f);

            GameObject gameui=GameObject.Find("GameUI"); //隱藏起始介面
            gameui.SetActive(false);

            GameObject readyUI=GameObject.Find("readytoroom"); //啟動準備介面
            readyUI.SetActive(true);

            GameObject g=GameObject.Find("playerinfoobject");
            savename svn=g.GetComponent<savename>();
            string tmpname = svn.username+"("+svn.character+")";

            GameObject localui=GameObject.Find("localplayerUI"); //啟動玩家介面
            localui.SetActive(true);
            
            initlocalui(); //初始玩家介面

            var tmpcolor= new Color
            (
                r: Random.Range(0f,1f),
                g: Random.Range(0f,1f),
                b: Random.Range(0f,1f),
                a: 1
            ); 
            // Debug.Log("start:"+gameObject.name);
            Cmdplayercount();
            Cmdsetupplayername(tmpname,tmpcolor);
        }
        GameObject gplayer=GameObject.Find("allplayer");
		gplayer.GetComponent<allplayer>().allplayerlist.Add(gameObject); //將新增的player存入playerlist
    }
    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer){
            GameObject me=GameObject.Find("ME");
            floatinginfo.transform.LookAt(me.transform);
            healthbar.transform.LookAt(me.transform);
        }
        else{
            // if(Input.GetButtonDown("Fire1")){
            //     gameObject.GetComponent<bulletcontroller>().Cmdfire();
            // }
            if(readyflag==true && isServer){ //遊戲倒數計時 由server執行
                time_f+=Time.deltaTime;
                time_i=(int)time_f;
                if(time_i!=timetmp){
                    GameObject readyUI=GameObject.Find("readytoroom");
                    readyUI.GetComponent<readytoroomscript>().Rpccountdown(5-time_i);
                    timetmp=time_i;
                }
                if(time_i==6){
                    Cmdplayerspawn(); //計時完 玩家重生
                    readyflag=false;
                }
            }
        }
    }

    private void initlocalui(){  //初始玩家介面
        // 初始技能
        GameObject skill=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        GameObject skill1=skill.transform.GetChild(0).gameObject;
        GameObject skill2=skill.transform.GetChild(1).gameObject;
        GameObject playerinfo=GameObject.Find("playerinfoobject");
        string character=playerinfo.GetComponent<savename>().character;
        if(character=="wizzard"){
            Sprite wizzard_skill1=Resources.Load<Sprite>("image/wizzard_skill1");
            Sprite wizzard_skill2=Resources.Load<Sprite>("image/wizzard_skill2");
            skill1.GetComponent<Image>().sprite=wizzard_skill1;
            skill2.GetComponent<Image>().sprite=wizzard_skill2;
        }
        else if(character=="assistant"){
            Sprite assistant_skill1=Resources.Load<Sprite>("image/assistant_skill1");
            Sprite assistant_skill2=Resources.Load<Sprite>("image/assistant_skill2");
            skill1.GetComponent<Image>().sprite=assistant_skill1;
            skill2.GetComponent<Image>().sprite=assistant_skill2;
        }
        else if(character=="assasins"){
            Sprite assasins_skill1=Resources.Load<Sprite>("image/assasins_skill1");
            Sprite assasins_skill2=Resources.Load<Sprite>("image/assasins_skill2");
            skill1.GetComponent<Image>().sprite=assasins_skill1;
            skill2.GetComponent<Image>().sprite=assasins_skill2;
        }
        else if(character=="hunter"){
            Sprite hunter_skill1=Resources.Load<Sprite>("image/hunter_skill1");
            Sprite hunter_skill2=Resources.Load<Sprite>("image/hunter_skill2");
            skill1.GetComponent<Image>().sprite=hunter_skill1;
            skill2.GetComponent<Image>().sprite=hunter_skill2;
        }

        // 初始玩家名稱
        GameObject name=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        name.GetComponent<TMP_Text>().text=playerinfo.GetComponent<savename>().username;
    }

    [Command]
    private void Cmdsetupplayername(string namevar,Color colorvar){
        playername=namevar;
        playernamecolor=colorvar;
    }
    [Command]
    public void Cmdplayercount(){ //有新的人加入
        GameObject me=GameObject.Find("ME");
        me.GetComponent<playerscript>().playernumber++; //計算人數

        GameObject ready=GameObject.Find("readytoroom");
        ready.GetComponent<readytoroomscript>().Rpcchangenowplayer(me.GetComponent<playerscript>().playernumber); //更新人數給每個client

        if(me.GetComponent<playerscript>().playernumber==2){ //當人數到達上限 倒數計時
            me.GetComponent<playerscript>().readyflag=true;    
        }
        
    }
    [Command]
    public void Cmdplayerspawn(){ //通知每個client重生
        GameObject allplayer=GameObject.Find("allplayer");
        List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist;

        GameObject ready=GameObject.Find("readytoroom");
        ready.GetComponent<readytoroomscript>().Rpchiddenreadyui();  //準備UI 隱藏

        foreach(var players in allplayerlist){
            players.GetComponent<playerscript>().Rpcplayerspawn(); //每個client出生在重生點
        }
    }
    [ClientRpc]
    public void Rpcplayerspawn(){ 
        if(isLocalPlayer){
            CharacterController cc = GetComponent(typeof(CharacterController)) as CharacterController;
            cc.enabled = false;     
            Transform spawn=NetworkManager.singleton.GetStartPosition();
            transform.position=spawn.position;
            transform.rotation=spawn.rotation;
            Debug.Log("x="+transform.position.x+" y="+transform.position.y+" z="+transform.position.z);
            cc.enabled=true;
        }
    }
}
