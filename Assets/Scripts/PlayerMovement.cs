using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

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
    public float normalHeight=0.93f, crouchHeight=0.4f;
    Vector3 velocity;
    bool isGrounded;
    public Animator anim;

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
        anim.SetFloat("Speed", Mathf.Abs(x + z));

        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetBool("Jump", true);
           // await Task.Delay(2000);
        }
        
         if(Input.GetKeyDown(KeyCode.C)&&isGrounded){
            controller.height = crouchHeight;
            anim.SetBool("Squat", true);
        }
        if(Input.GetKeyUp(KeyCode.C)){
            controller.height = normalHeight;
            anim.SetBool("Squat", false);
        }

       // anim.SetBool("Jump", false);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
         
        if(rb.position.y < platform_y){
            SceneManager.LoadScene(2);
        }
    }


}