using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillscript : MonoBehaviour
{
    private int skill1tmp=-1,skill2tmp=-1;
    private int skill1_i,skill2_i;
    private float skill1_f,skill2_f;
    bool skill1_flag=false;
    bool skill2_flag=false;
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
    }
    private void useskill1(){

    }
    private void useskill2(){

    }
}
