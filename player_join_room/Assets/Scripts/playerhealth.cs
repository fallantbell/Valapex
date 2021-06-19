using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class playerhealth : NetworkBehaviour
{
    public const int maxhealth=100;
    [SyncVar(hook=nameof(changehealth))] //同步血量給所有client
    public int currenthealth=maxhealth;
    public RectTransform healthbar;
    
    public bool damageflag=false; //準備階段不會受到傷害
    public void takedamage(int amount,string damageperson){ //  受到傷害
        if(!isServer){
            return;
        }
        GameObject me=GameObject.Find("ME");
        if(me.GetComponent<playerhealth>().damageflag==true){
            currenthealth-=amount;
        }
        
        if(currenthealth<=0){      // 死掉
            recordkill(damageperson);

            currenthealth=maxhealth;
            gameObject.GetComponent<playerscript>().Rpcplayerspawn();
            Debug.Log("dead");
        }
        
    }

    private void recordkill(string damageperson){
        // Debug.Log("damageperson "+damageperson);
        GameObject allplayer=GameObject.Find("allplayer");
        List<GameObject> allplayerlist=allplayer.GetComponent<allplayer>().allplayerlist;

        foreach(var players in allplayerlist){  // 尋找射出子彈的人 紀錄擊殺
            // Debug.Log("players: "+players.name);
            if(players.name==damageperson){
                players.GetComponent<playerhealth>().Rpcaddkill(); 
            }
        }

    }
    public void changehealth(int oldValue,int newValue){              //改變血條
        healthbar.sizeDelta=new Vector2(newValue,healthbar.sizeDelta.y);
        if(isLocalPlayer){
            RectTransform localhealthbar=GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
            localhealthbar.sizeDelta=new Vector2(newValue,localhealthbar.sizeDelta.y);
        }
    }

    [ClientRpc]
    public void Rpcaddkill(){
        if(isLocalPlayer){
            gameObject.GetComponent<playerscript>().addkill();
        }
        else{
            gameObject.GetComponent<playerscript>().synckill();
        }
    }
}
