using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAi : MonoBehaviour
{
    public int health = 100;
    public int armor = 100;
    public void TakeDamage(int damage){
        if(armor > 0){
            armor -= damage;
        }
        else{
            health -= damage;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
