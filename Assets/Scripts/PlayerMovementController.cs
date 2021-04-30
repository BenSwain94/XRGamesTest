using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    CharacterController con;

    private float playerSpeed;
    private float gravity = -19.62f;
    [SerializeField] private float walkSpeed, sprintSpeed, cruchSpeed, jumpHeight;

    Transform groundCheck;

    Animator anim;

    Vector3 gravityVelocity;

    [SerializeField] LayerMask groundMask;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cache();
    }

    /// <summary>
    /// Cache components etc
    /// </summary>
    void Cache()
    {
        con = GetComponent<CharacterController>();
        groundCheck = transform.GetChild(transform.childCount - 1);
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    /// <summary>
    /// Handle movement & inputs
    /// </summary>
    void MovePlayer()
    {
        //Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("xSpeed", x);
        anim.SetFloat("zSpeed", z);

        Vector3 move = transform.right * x + transform.forward * z;
        
        //Stop the speed multiplying when receiving both inputs
        if(x != 0 && z != 0)
            move.Normalize();

        Debug.Log(move);

        Jumping();
        Sprint();

        gravityVelocity.y += gravity * Time.deltaTime;

        con.Move(((move * playerSpeed) + gravityVelocity ) * Time.deltaTime);
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Sprint()
    {
        if (isGrounded)
        {
            playerSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            if(Input.GetKeyDown(KeyCode.LeftShift))
                anim.SetBool("Sprinting", true);
            if (Input.GetKeyUp(KeyCode.LeftShift))
                anim.SetBool("Sprinting", false);
        }
    }
}
