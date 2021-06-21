using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class seeskillinfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject game=GameObject.Find("GameUI");
        game.GetComponent<playerentry>().closeskillinfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData) {  // 滑鼠進入技能 開啟簡介
        // Debug.Log("mouse enter");
        GameObject game=GameObject.Find("GameUI");
        game.GetComponent<playerentry>().showskillinfo(this.name);
    }
    public void OnPointerExit(PointerEventData eventData) {  // 滑鼠離開技能 關閉簡介
        GameObject game=GameObject.Find("GameUI");
        game.GetComponent<playerentry>().closeskillinfo();
    }
}
