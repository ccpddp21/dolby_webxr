using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //create basic motion controls
    public float speed = 5;
    public float jumpSpeed = 8;
    public float gravity = 20;
    public float rotateSpeed = 5;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
