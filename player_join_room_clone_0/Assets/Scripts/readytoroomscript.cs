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
        nowplayer.SetText("Player:"+number.ToString());
    }
    [ClientRpc]
    public void Rpccountdown(int number){  // 遊戲開始前倒數
        // Debug.Log("receive");
        countdown.SetText("Game Start:"+number.ToString());
    }
    [ClientRpc]
    public void Rpchiddenreadyui(){
        gameObject.SetActive(false);
    }
}
