using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PLAYER_Movement : MonoBehaviour
{
    public int walkingSpeed, runningSpeed;
    public int movementSpeed;
    public float turnSmoothTime;

    public bool isSprinting;
   

    //Jump Stuff
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;

    CharacterController charController;
    public Transform playerCamera;



    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() 
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveDir = new Vector3(inputX, 0, inputZ).normalized;
        movementSpeed = isSprinting ? runningSpeed : walkingSpeed;


        Jumping(moveDir);

        //Rotate player in direction of camera
        transform.rotation = Quaternion.Euler(0f, playerCamera.eulerAngles.y, 0f);
     
        Vector3 moveAmount = moveDir * movementSpeed;
        velocity.y += gravity * Time.deltaTime;
        moveAmount.y = velocity.y;
        transform.TransformDirection(moveAmount);

        Vector3 direction = transform.TransformDirection(moveAmount);
        charController.Move(direction *  Time.deltaTime);
    }

    void Jumping(Vector3 moveDir) 
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && moveDir.magnitude <= 0.05f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }
}
