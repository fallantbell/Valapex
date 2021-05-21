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
    public void takedamage(int amount){ //  受到傷害
        if(!isServer){
            return;
        }
        currenthealth-=amount;
        if(currenthealth<=0){      // 死掉
            currenthealth=maxhealth;
            gameObject.GetComponent<playerscript>().Rpcplayerspawn();
            Debug.Log("dead");
        }
        
    }
    public void changehealth(int oldValue,int newValue){              //改變血條
        healthbar.sizeDelta=new Vector2(newValue,healthbar.sizeDelta.y);
    }
}
