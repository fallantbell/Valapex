using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private LayerMask mask;
    public float groundDistance = 0.4f;
    public float moveSpeed = 10.0f;
    bool isGrounded;
    private void Start()
    {
        mask = LayerMask.GetMask("Ground");
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, mask);
        if(isGrounded){
            // player.position.y = 0;
        }
        // ���oWASD�ƾ�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // ���oVector3(LR value, 0, ForwardAndBack value)
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        yield return null;
    }
}
