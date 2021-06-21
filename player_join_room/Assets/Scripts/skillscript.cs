using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class skillscript : MonoBehaviour
{
    private int skill1tmp=-1,skill2tmp=-1;
    private int skill1_i,skill2_i;
    private float skill1_f,skill2_f,teleport_f;
    bool skill1_flag=false;
    bool skill2_flag=false;
    bool teleportflag=false;
    public int skill1waittime=4;
    public int skill2waittime=5;
    public RectTransform skill1cool;
    public RectTransform skill2cool;
    

    public void skill1cooldown(int v){ //技能冷卻動畫
        skill1cool.sizeDelta=new Vector2(skill1cool.sizeDelta.x,v);
    }
    public void skill2cooldown(int v){ //技能冷卻動畫
        skill2cool.sizeDelta=new Vector2(skill2cool.sizeDelta.x,v);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.C) && skill1_flag==false){ // 技能不在冷卻且按下C 發動技能1
            // Debug.Log("use skill1");
            useskill1();
            skill1_i=0;
            skill1_f=0;
            skill1tmp=-1;
            skill1_flag=true;
        }
        if(Input.GetKeyDown(KeyCode.V) && skill2_flag==false){ // 技能不在冷卻且按下V 發動技能2
            // Debug.Log("use skill2");
            useskill2();
            skill2_f=0;
            skill2_i=0;
            skill2tmp=-1;
            skill2_flag=true;
        }
        if(skill1_flag==true){  // 技能1冷卻計時
            skill1_f+=Time.deltaTime;
            skill1_i=(int)skill1_f;
            if(skill1_i!=skill1tmp){
                skill1cooldown(100-(100/skill1waittime)*skill1_i); 
                skill1tmp=skill1_i;
            }
            if(skill1_i==skill1waittime){
                skill1_flag=false;
            }
        }
        if(skill2_flag==true){ // 技能2冷卻計時
            skill2_f+=Time.deltaTime;
            skill2_i=(int)skill2_f;
            if(skill2_i!=skill2tmp){
                skill2cooldown(100-(100/skill2waittime)*skill2_i);
                skill2tmp=skill2_i;
            }
            if(skill2_i==skill2waittime){
                skill2_flag=false;
            }
        }
        teleport_f+=Time.deltaTime;
        if(teleport_f>0.3 && teleportflag==true){
            GameObject me=GameObject.Find("ME");
            me.GetComponent<FirstPersonController>().is_skill=false;
            me.GetComponent<playerscript>().Cmdghost(0);
            teleportflag=false;
        }
    }
    private void useskill1(){
        GameObject playerinfo=GameObject.Find("playerinfoobject");
        string character=playerinfo.GetComponent<savename>().character;
        GameObject me=GameObject.Find("ME");
        if(character=="assasins"){ // 分身
            me.GetComponent<playerscript>().Cmdduplicate(); // 同步分身到所有client
        }
        if(character == "assistant")
        {
            me.GetComponent<assistantskill>().Cmdaddhealth(30);
        }
        if(character == "hunter")
        {
            me.GetComponent<hunterskill>().changelight();
            GameObject g=GameObject.Find("allplayer");
            foreach(GameObject player in g.GetComponent<allplayer>().allplayerlist)
            {
                player.GetComponent<changeMaterial>().enabled=true;
            }

        }
    }
    private void useskill2(){
        GameObject playerinfo=GameObject.Find("playerinfoobject");
        string character=playerinfo.GetComponent<savename>().character;
        GameObject me=GameObject.Find("ME");
        if(character=="assasins"){
            me.GetComponent<FirstPersonController>().is_skill=true;
            me.GetComponent<playerscript>().Cmdghost(1); // 同步殘影到所有client
            teleport_f=0;
            teleportflag=true;
        }
        if(character == "assistant")
        {           
            me.GetComponent<assistantskill>().Cmdaddshield(30);
        }
        if(character == "hunter")
        {
            me.GetComponent<hunterskill>().Cmdsettrap();
        }
    }
}
// server -> server 正常
// server -> client 正常
// client -> client 不正常
// client -> server 不正常