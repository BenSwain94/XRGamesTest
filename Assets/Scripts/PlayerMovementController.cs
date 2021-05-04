using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //Input variables
    private float xInput, zInput;
    //Distance between the player and ground below
    private float groundDist;

    //Variables for resetting capsule collider when uncrouched
    private float uncrouchedHeight = 2;
    Vector3 uncrouchedCenter = new Vector3(0, 1, 0);

    //Player movement speed variables
    public float playerSpeed;
    [SerializeField] private float walkSpeed, sprintSpeed, crouchSpeed, jumpHeight;

    //Variables and components for checking if player is correctly grounded etc
    [SerializeField] Transform leftFoot, rightFoot, groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask rayMask;
    [SerializeField] bool isGrounded;

    Animator anim;
    Rigidbody rb;
    RaycastHit hit;
    CapsuleCollider coll;

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
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();

        playerSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Sprint();
        Jumping();
        Crouch();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Handle movement & inputs
    /// </summary>
    void MovePlayer()
    {
        //Performs two checkboxes at either feet and if both are colliding with something then player is grounded
        isGrounded = Physics.CheckBox(leftFoot.position, new Vector3(.11f, .09f, .09f), transform.root.rotation, groundMask) || Physics.CheckBox(rightFoot.position, new Vector3(.11f, .09f, .09f), transform.root.rotation, groundMask);

        if (!isGrounded)
        {
            anim.SetBool("Airborne", true);

            if (Physics.Raycast(groundCheck.position, -transform.up * 2, out hit, 2, rayMask))
            {
                groundDist = hit.distance;
                anim.SetFloat("gDistance", groundDist);
            }
        }
        else
        {
            anim.SetBool("Airborne", false);
            anim.SetBool("Jumping", false);
        }

        anim.SetFloat("xSpeed", xInput);
        anim.SetFloat("zSpeed", zInput);

        Vector3 movement = transform.right * xInput + transform.forward * zInput;

        //Stop the speed multiplying when receiving both inputs
        if (xInput != 0 && zInput != 0)
            movement.Normalize();

        rb.MovePosition(transform.position + movement * playerSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles jumping inputs and sets animator variables accordingly
    /// </summary>
    void Jumping()
    {
        //Jump on space input but only if player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            anim.SetBool("Jumping", true);
            anim.SetBool("Sprinting", false);
        }
    }

    /// <summary>
    /// Handles sprinting input and sets player speed and animator variables accordingly
    /// </summary>
    void Sprint()
    {
        if (anim.GetBool("Crouching"))
            return;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            anim.SetBool("Sprinting", true);
            playerSpeed = sprintSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Sprinting", false);
            playerSpeed = walkSpeed;
        }
    }


    /// <summary>
    /// Handles crouching input and sets player speed and animator variables accordingly
    /// </summary>
    void Crouch()
    {
        if (isGrounded)
        {
            //Crouching
            if (Input.GetKey(KeyCode.LeftControl))
            {
                SetCrouchVars(crouchSpeed, new Vector3(0, .6f, 0), 1.2f, true);
            }
            //Uncrouching
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                //If there is nothing above the player, allow uncrouch
                if (!UncrouchCheck(1.4f))
                {
                    SetCrouchVars(walkSpeed, uncrouchedCenter, uncrouchedHeight, false);
                }
            }

            if (anim.GetBool("Crouching") && !Input.GetKey(KeyCode.LeftControl))
            {
                //Keep player at crouch speed, so player stays crouched while not pressing ctrl, until no object is above
                playerSpeed = crouchSpeed;

                //If there is nothing above the player, allow uncrouch
                if (!UncrouchCheck(1.4f))
                {
                    SetCrouchVars(walkSpeed, uncrouchedCenter, uncrouchedHeight, false);
                }
            }
        }
    }

    /// <summary>
    /// Sphere check above the players head returns true if collides, false if not
    /// </summary>
    bool UncrouchCheck(float height)
    {
        if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y + height, transform.position.z), .25f, rayMask))
            return true;
        else
            return false;
    }

    void SetCrouchVars(float newSpeed, Vector3 center, float height, bool animBool)
    {
        playerSpeed = newSpeed;
        coll.center = center;
        coll.height = height;
        anim.SetBool("Crouching", animBool);
        anim.SetBool("Sprinting", false);
    }
}
