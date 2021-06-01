using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
<<<<<<< Updated upstream
    Camera fpsCam;
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
=======
    // Start is called before the first frame update
    void Start()
    {
>>>>>>> Stashed changes
        
    }

    // Update is called once per frame
<<<<<<< Updated upstream
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(fpsCam.transform.forward);
=======
    void Update()
    {
        
>>>>>>> Stashed changes
    }
}
