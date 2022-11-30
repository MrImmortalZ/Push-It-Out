using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
   
    public float speed = 12f;
    public Rigidbody rb;
    public float platform_y;
    public float gravity = -10f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float normalHeight, crouchHeight;
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if(isGrounded && velocity.y<0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&&isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
         if(Input.GetKeyDown(KeyCode.C)&&isGrounded){
            controller.height = crouchHeight;
        }
        if(Input.GetKeyUp(KeyCode.C)){
            controller.height = normalHeight;
         }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
         
        if(rb.position.y < platform_y){
            // Gameover UI
        }
    }


}