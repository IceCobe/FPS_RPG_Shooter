using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravityMultiplier = 2f;
    public float groundDistance = 0.2f;
    
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        // Checks to see if we're on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If we're on the ground stay there
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        // Variable gathering
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move the character
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jump Implementation
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityMultiplier * -9.81f);
        }

        // Gravity
        velocity.y += gravityMultiplier * -9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
