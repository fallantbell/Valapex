using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class readytoroomscript : NetworkBehaviour
{
    public TMP_Text nowplayer;
    public TMP_Text countdown;
    [ClientRpc]
    public void Rpcchangenowplayer(int number){  // 顯示現有玩家人數
        nowplayer.text="Player:"+number.ToString();
    }
    [ClientRpc]
    public void Rpccountdown(int number){  // 遊戲開始前倒數
        // Debug.Log("receive");
        countdown.text="Game Start:"+number.ToString();
    }
    [ClientRpc]
    public void Rpchiddenreadyui(){
        gameObject.SetActive(false);
        GameObject.Find("localplayerUI").transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }
}
