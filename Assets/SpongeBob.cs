using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class SpongeBob : MonoBehaviour
{
    CharacterController characterController;

    public Vector3 scaleToUse = new Vector3(-1.265f,1.265f,1);
    public Vector3 scaleToUse2 = new Vector3(1.265f,1.265f,1);
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public bool flipped = false;
    public bool allowJump = false;
    public float zPosition = -5f;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumping = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float direction;
        if(GlobalVariables.global.joystick.gameObject.activeSelf)
            direction = GlobalVariables.global.joystick.GetInputDirection().x;
        else
            direction = Input.GetAxis("Horizontal");
        if(direction != 0) Debug.Log(direction);
        if(!GlobalVariables.global.busy)
        {
            if(direction < 0)
            {
                transform.localScale = scaleToUse;
                transform.Find("body").gameObject.SetActive(false);
                flipped = true;
            }
            else if(direction > 0)
            {
                transform.localScale = scaleToUse2;
                transform.Find("body").gameObject.SetActive(true);
                flipped = false;
            }
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                moveDirection = new Vector3(direction, 0.0f, 0f); //Input.GetAxis("Vertical"));
                moveDirection *= speed;

                if (Input.GetButton("Jump"))    
                    isJumping = true;
                if(isJumping && allowJump)
                {
                    moveDirection.y = jumpSpeed;
                    isJumping = false;
                }
            }
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        //don't get stuck out of bounds
        if(transform.position.z != zPosition)
        {
            moveDirection.z = (zPosition - transform.position.z);
        }
        
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
	public void jump(){
       isJumping = true;
	}
}
