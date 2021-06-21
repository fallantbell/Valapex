using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Camera fpsCam;
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(fpsCam.transform.forward);
    }
}
