using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLook : MonoBehaviour
{
    //The period to wait until resetting the input value.Set this as low as possible, without entering stuttering

    public Vector2 sensitivity;

    private Vector2 rotation;
    private float rotationy = 0f;
    // Start is called before the first frame update
    Vector2 GetInput(){
        
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );
        // if((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod){
        //     lastInput = input;
        //     inputLagTimer = 0;
        // }
        return input;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensitivity;
        
        rotation = wantedVelocity * Time.deltaTime;
        rotationy += rotation.y;
        rotationy = Mathf.Clamp(rotationy, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationy, 0, 0);
    }
}
