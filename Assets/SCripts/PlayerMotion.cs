using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float vel = 3;
    bool crouching = false;
    float crouchTimer = 1;
    bool lerpCrouch = false;
    bool sprinting = false;
    public AudioSource footSteps;
    private float ogSpeed;

    public float jumpHeight = 3;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        ogSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }
    
    //Recive el input de la clase InputManager.cs y los aplica a el controlador del personaje
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        Vector3 moveSpeed = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;
        controller.Move(moveSpeed);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        if (moveSpeed.magnitude > 0.01 && playerVelocity.y == -2f) footSteps.enabled = true;
        else footSteps.enabled = false;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity); 
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = ogSpeed + vel;
        else
            speed = ogSpeed;
    }

    public void Destroy()
    {
        Debug.Log("Hola");
        // Find the ChickenDestroyer script on the player GameObject
        ChickenDestroyer chickenDestroyer = GetComponent<ChickenDestroyer>();
        if (chickenDestroyer != null)
        {
            // Trigger the chicken destruction logic
            chickenDestroyer.DestroyChicken();
        }
    }

    public float getPlayerSpeed()
    {
        return speed;
    }

    public void setPlayerSpeed(float speed)
    {
        this.speed = speed;
    }
}
