using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class player_clone_control : MonoBehaviour
{
    public GameObject floatinginfo;
    public GameObject healthbar;
    public TextMesh nametext;
    public RectTransform heal;

    public Vector3 rot;
    private float speed=5.0f;
    private float age;
    private float maxage=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        age=0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject me=GameObject.Find("ME");
        floatinginfo.transform.LookAt(me.transform);
        healthbar.transform.LookAt(me.transform);

        Vector3 move = transform.forward ;

        gameObject.transform.position+=move * speed * Time.deltaTime;
        // Debug.Log("rot:"+rot);
        age+=Time.deltaTime;
        if(age>maxage){
            Destroy(gameObject);
        }
    }
}
