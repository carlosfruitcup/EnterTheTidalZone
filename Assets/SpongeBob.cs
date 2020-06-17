using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float sprintSpeed = 12f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public bool flipped = false;
    public bool allowJump = false;
    public float zPosition = -5f;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumping = false;
    private float ogSpeed;
    public int health = 5;
    private int healthBefore = 5;
    public bool invincible = false;
    private bool invincibleStarted = false;
    private int invincibleCounter = 0;
    private bool dying = false;
    public SpriteRenderer[] sprites;
	private string[] cheatCode;
	private int index;
	Animator m_Animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ogSpeed = speed;
		m_Animator = gameObject.GetComponent<Animator>();
		cheatCode = new string[] { "s", "p", "a", "n", "g", "o" };
		index = 0;
    }

    void Update()
    {
		if (Input.anyKeyDown) {
				// Check if the next key in the code is pressed
			if (Input.GetKeyDown(cheatCode[index])) {
				// Add 1 to index to check the next key in the code
				index++
			}
		}
		if (index == cheatCode.Length) {
			if(invincible){
				invincible = false;
			} else {
			invincible = true;
			}
		}
        // Wrong key entered, we reset code typing
		else {
			index = 0;    
		}
        if(health > 0)
        {
            if(health < healthBefore)
            {
                if(invincible)
                    health = healthBefore;
                else
                {
                    GlobalVariables.global.rewindTime = true;
                    invincible = true;
                }
            }
            healthBefore = health;
            if(invincible && !invincibleStarted)
            {
                Debug.Log("now invincible");
                StartCoroutine(Invincible());
                invincibleCounter = 200;
            }
            if(invincibleCounter > 0)
            {
                invincibleCounter -= 1;
                if(invincibleCounter == 0)
                    invincible = false;
            }
            else
            {
                invincible = false;
            }
            float direction;
            if(GlobalVariables.global.joystick.gameObject.activeSelf)
                direction = GlobalVariables.global.joystick.GetInputDirection().x;
            else
                direction = Input.GetAxis("Horizontal");
			if(direction != 0){
				m_Animator.SetBool("walk", true);
			} else {
				m_Animator.SetBool("walk", false);
			}
            //if(direction != 0) Debug.Log(direction);
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
                    if (Input.GetButton("Sprint"))    
                        speed = sprintSpeed;
                    else
                        speed = ogSpeed;
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
            if(characterController.enabled)
                characterController.Move(moveDirection * Time.deltaTime);
        }
        else if(!dying)
        {
            StartCoroutine(Death());
        }
    }
	public void jump(){
       isJumping = true;
	}
    IEnumerator Invincible()
    {
        invincibleStarted = true;
        while(invincible)
        {
            yield return new WaitForSeconds(0.15f);
            for(int i=0;i < sprites.Length; i++)
            {
                sprites[i].color = Color.clear;
            }
            yield return new WaitForSeconds(0.15f);
            for(int i=0;i < sprites.Length; i++)
            {
                sprites[i].color = Color.gray;
            }
        }
        for(int i=0;i < sprites.Length; i++)
        {
            sprites[i].color = Color.white;
        }
        invincibleStarted = false;
        yield return null;
    }
    IEnumerator Death()
    {
        dying = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        Debug.Log(col.gameObject.name);
        if(col.transform.tag == "Enemy")
        {
            BaseEnemy enemy = col.gameObject.GetComponent<BaseEnemy>();
            if(!invincibleStarted)
                health -= enemy.damage;
        }
    }
}
