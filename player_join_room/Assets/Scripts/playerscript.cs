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

    public List<int> characternum;

    private float time_f=0;
    private int time_i=0; //用來計算準備倒數計時時間
    private int timetmp=-1;
    bool readyflag=false; //用來判斷準備開始

    [SyncVar(hook = nameof(playernamechange))]
    public string playername;

    [SyncVar(hook = nameof(colornamechange))]
    private Color playernamecolor;
    private FirstPersonController fpsController;
	private Transform playerCameraTransform;
	private Camera playerCamera;
	private MouseLook mouseLook;
	private GunSystem gunSystem;
	private WeaponManager weaponManager;
	private AudioListener playerAudioListener;

	private GameObject[] gameObjects;

    public int killnum; //擊殺數

    public Animator anim;

    public void addkill(){
        killnum+=1;
        GameObject killtext=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(5).gameObject;
        killtext.GetComponent<TMP_Text>().text="kill:"+killnum.ToString();
    }

    public void synckill(){
        killnum+=1;
    }

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

        if(isLocalPlayer){
            initcharacter(); //初始玩家角色
        }
        
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
        if(Input.GetKeyDown(KeyCode.Tab)){
            GameObject allplayer=GameObject.Find("allplayer");
            List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist; 

            int tmp=0;
            foreach(var players in allplayerlist){ 
                if(players.name=="ME") continue;
                GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text="kill:"+players.GetComponent<playerscript>().killnum;
                tmp+=1;
            }
            GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.SetActive(false);
        }
    }

    private void initcharacter(){
        GameObject playerinfo=GameObject.Find("playerinfoobject");
        string character=playerinfo.GetComponent<savename>().character;
        if(character=="wizzard"){
            Cmdcharacter(6);
        }
        else if(character=="assistant"){
            Cmdcharacter(7);
        }
        else if(character=="assasins"){
            Cmdcharacter(8);
        }
        else if(character=="hunter"){
            Cmdcharacter(5);
        }
    }

    [Command]
    public void Cmdcharacter(int num){
        GameObject me=GameObject.Find("ME");
        me.GetComponent<playerscript>().characternum.Add(num); //將每個玩家角色存入 me 的characternum 陣列
        GameObject allplayer=GameObject.Find("allplayer");
        List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist; 

        int tmp=0;
        foreach(var players in allplayerlist){ // 對每個玩家做clientrpc 通知每個玩家的角色
            players.GetComponent<playerscript>().Rpccharacter(me.GetComponent<playerscript>().characternum[tmp]);
            tmp+=1;
        }

        // Rpccharacter(num);
    }

    [ClientRpc]
    public void Rpccharacter(int num){
        // Debug.Log("name:"+gameObject.name);
        // Debug.Log("number:"+num);
        gameObject.transform.GetChild(num).gameObject.SetActive(true);

        if(num==5){
            anim.avatar=Resources.Load<Avatar>("hunter");
        }
        else if(num==6){
            anim.avatar=Resources.Load<Avatar>("wizard");
        }
        else if(num==7){
            anim.avatar=Resources.Load<Avatar>("assistant");
        }
        else if(num==8){
            anim.avatar=Resources.Load<Avatar>("assasins");
        }
        
        if(isLocalPlayer){
            GameObject allplayer=GameObject.Find("allplayer");
            List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist; 

            int tmp=0;
            foreach(var players in allplayerlist){ 
                Cmddebug(playername,players.GetComponent<playerscript>().playername);
                if(players.name=="ME") continue;
                string tmpname=players.GetComponent<playerscript>().playername;
                string tmpname2="";
                string character="";
                bool flag=false;
                
                foreach(char c in tmpname){
                    if(c=='('){
                        flag=true;
                        continue;
                    }
                    if(c==')'){
                        break;
                    }
                    if(flag==false){
                        tmpname2+=c;
                    }
                    else{
                        character+=c;
                    }
                }
                Debug.Log("character:"+character);
                GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text=tmpname2;
                if(character=="wizzard"){
                    Sprite wizard_small=Resources.Load<Sprite>("image/wizard_small");
                    GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.GetComponent<Image>().sprite=wizard_small;
                }
                else if(character=="assistant"){
                    Sprite assistant_small=Resources.Load<Sprite>("image/assistant_small");
                    GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.GetComponent<Image>().sprite=assistant_small;
                }
                else if(character=="assasins"){
                    Sprite assasins_small=Resources.Load<Sprite>("image/assasins_small");
                    GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.GetComponent<Image>().sprite=assasins_small;
                }   
                else if(character=="hunter"){
                    Sprite hunter_small=Resources.Load<Sprite>("image/hunter_small");
                    GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(tmp).gameObject.GetComponent<Image>().sprite=hunter_small;
                }
                // Cmddebug(playername,tmpname2);
                tmp+=1;
            }
        }
    }

    [Command]
    public void Cmddebug(string a,string b){
        Debug.Log("dddddddddddd    "+a+": "+b);
    }

    private void initlocalui(){  //初始玩家介面
        GameObject skill=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        GameObject playerimage=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        GameObject skill1=skill.transform.GetChild(0).gameObject;
        GameObject skill2=skill.transform.GetChild(1).gameObject;
        GameObject playerinfo=GameObject.Find("playerinfoobject");
        string character=playerinfo.GetComponent<savename>().character;
        if(character=="wizzard"){
            Sprite wizzard_skill1=Resources.Load<Sprite>("image/wizzard_skill1");
            Sprite wizzard_skill2=Resources.Load<Sprite>("image/wizzard_skill2");
            Sprite wizard_small=Resources.Load<Sprite>("image/wizard_small");
            skill1.GetComponent<Image>().sprite=wizzard_skill1;
            skill2.GetComponent<Image>().sprite=wizzard_skill2;
            playerimage.GetComponent<Image>().sprite=wizard_small;
        }
        else if(character=="assistant"){
            Sprite assistant_skill1=Resources.Load<Sprite>("image/assistant_skill1");
            Sprite assistant_skill2=Resources.Load<Sprite>("image/assistant_skill2");
            Sprite assistant_small=Resources.Load<Sprite>("image/assistant_small");
            skill1.GetComponent<Image>().sprite=assistant_skill1;
            skill2.GetComponent<Image>().sprite=assistant_skill2;
            playerimage.GetComponent<Image>().sprite=assistant_small;
        }
        else if(character=="assasins"){
            Sprite assasins_skill1=Resources.Load<Sprite>("image/assasins_skill1");
            Sprite assasins_skill2=Resources.Load<Sprite>("image/assasins_skill2");
            Sprite assasins_small=Resources.Load<Sprite>("image/assasins_small");
            skill1.GetComponent<Image>().sprite=assasins_skill1;
            skill2.GetComponent<Image>().sprite=assasins_skill2;
            playerimage.GetComponent<Image>().sprite=assasins_small;
        }
        else if(character=="hunter"){
            Sprite hunter_skill1=Resources.Load<Sprite>("image/hunter_skill1");
            Sprite hunter_skill2=Resources.Load<Sprite>("image/hunter_skill2");
            Sprite hunter_small=Resources.Load<Sprite>("image/hunter_small");
            skill1.GetComponent<Image>().sprite=hunter_skill1;
            skill2.GetComponent<Image>().sprite=hunter_skill2;
            playerimage.GetComponent<Image>().sprite=hunter_small;
        }

        // 初始玩家名稱
        GameObject name=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        name.GetComponent<TMP_Text>().text=playerinfo.GetComponent<savename>().username;
    }

    [Command]
    private void Cmdsetupplayername(string namevar,Color colorvar){
        playername=namevar;
        playernamecolor=colorvar;
        Rpcsyncplayername(playername);
    }

    [ClientRpc]
    private void Rpcsyncplayername(string namevar){
        playername=namevar;
    }

    [Command]
    public void Cmdplayercount(){ //有新的人加入
        GameObject me=GameObject.Find("ME");
        me.GetComponent<playerscript>().playernumber++; //計算人數

        GameObject ready=GameObject.Find("readytoroom");
        ready.GetComponent<readytoroomscript>().Rpcchangenowplayer(me.GetComponent<playerscript>().playernumber); //更新人數給每個client

        if(me.GetComponent<playerscript>().playernumber==4){ //當人數到達上限 倒數計時
            me.GetComponent<playerscript>().readyflag=true;    
        }
        
    }
    [Command]
    public void Cmdplayerspawn(){ //通知每個client重生
        GameObject allplayer=GameObject.Find("allplayer");
        List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist;

        GameObject ready=GameObject.Find("readytoroom");
        ready.GetComponent<readytoroomscript>().Rpchiddenreadyui();  //準備UI 隱藏

        gameObject.GetComponent<playerhealth>().damageflag=true; // 遊戲開始 可以受到傷害

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

    [Command]
    public void Cmdghost(int num){
        Rpcghost(num);
    }

    [ClientRpc]
    public void Rpcghost(int num){
        if(num==1){   // 開啟殘影
            gameObject.GetComponent<GhostShadow>().enabled=true;
        }
        else if(num==0){  // 關閉殘影
            gameObject.GetComponent<GhostShadow>().enabled=false;
        }
    }
    [Command]
    public void Cmdduplicate(){
        GameObject instance = Instantiate (Resources.Load ("player_clone")) as GameObject;
        instance.name = "player_clone";
        instance.transform.localRotation = gameObject.transform.localRotation;
        instance.transform.position = gameObject.transform.position+transform.forward*2;

        instance.GetComponent<player_clone_control>().heal.sizeDelta=new Vector2(gameObject.GetComponent<playerhealth>().currenthealth,instance.GetComponent<player_clone_control>().heal.sizeDelta.y);
        instance.GetComponent<player_clone_control>().nametext.text=nametext.text;

        NetworkServer.Spawn (instance);
    }
}
