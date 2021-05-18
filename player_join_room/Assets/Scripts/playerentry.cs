using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerentry : MonoBehaviour
{
    public Button name_bt,wizzard_bt,assistant_bt,assasins_bt,hunter_bt;
    public TMP_InputField nameinput;
    public GameObject ice_text_g,fire_text_g,recover_text_g,shield_text_g,dupicate_text_g,rush_text_g,see_text_g,trap_text_g;
    public GameObject skillinfo;
    private bool nameconfirmflag=false;
    //check whether the name has been input

    void Start()
    {
        name_bt.interactable=false;
    }

    void Update()
    {
    
    }

    public void showskillinfo(string skill){ // 開啟技能簡介
        skillinfo.SetActive(true);
        if(skill=="ice"){
            ice_text_g.SetActive(true);
        }
        else if(skill=="fire"){
            fire_text_g.SetActive(true);
        }
        else if(skill=="recover"){
            recover_text_g.SetActive(true);
        }
        else if(skill=="shield"){
            shield_text_g.SetActive(true);
        }
        else if(skill=="dupicate"){
            dupicate_text_g.SetActive(true);
        }
        else if(skill=="rush"){
            rush_text_g.SetActive(true);
        }
        else if(skill=="see"){
            see_text_g.SetActive(true);
        }
        else if(skill=="trap"){
            trap_text_g.SetActive(true);
        }
    }
    public void closeskillinfo(){  //關閉技能簡介
        skillinfo.SetActive(false);
        ice_text_g.SetActive(false);
        fire_text_g.SetActive(false);
        recover_text_g.SetActive(false);
        shield_text_g.SetActive(false);
        dupicate_text_g.SetActive(false);
        rush_text_g.SetActive(false);
        see_text_g.SetActive(false);
        trap_text_g.SetActive(false);
    }

    public void setplayername(){
        string tmpname=nameinput.text;
        if(!string.IsNullOrEmpty(tmpname) && nameconfirmflag==false){ 
            name_bt.interactable=true;
        }
        else{  //  輸入名稱為空或是已經confirm名稱 confirm button 設false
            name_bt.interactable=false;
        }
        
    }
    public void confirmusername(){ //確認名字
        name_bt.interactable=false;
        nameconfirmflag=true;
        GameObject g=GameObject.Find("playerinfoobject");
        savename svn=g.GetComponent<savename>(); 
        svn.username=nameinput.text; //紀錄player名稱
    }
    public void confirmcharacter(string character){ //確認角色
        GameObject g=GameObject.Find("playerinfoobject");
        savename svn=g.GetComponent<savename>(); 
        svn.character=character; //紀錄選擇角色
        disablecharacterbt();
    }
    private void disablecharacterbt(){ //選擇一隻角色後不能選其他隻
        wizzard_bt.interactable=false;
        assasins_bt.interactable=false;
        assistant_bt.interactable=false;
        hunter_bt.interactable=false;
    }
}
