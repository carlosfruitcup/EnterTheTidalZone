using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
/// <summary>Used for the main character to move and interact with the environment, enemies, etc.
/// <para>This is a MonoBehaviour class. </para>
/// <seealso cref="BaseEnemy"/>
/// </summary>
public class SpongeBob : MonoBehaviour
{
    CharacterController characterController;

    public Vector3 scaleToUse = new Vector3(-1.265f, 1.265f, 1);
    public Vector3 scaleToUse2 = new Vector3(1.265f, 1.265f, 1);
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
    public SpriteRenderer eyes;
    Animator m_Animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ogSpeed = speed;
        m_Animator = gameObject.GetComponent<Animator>();
        if (eyes)
            StartCoroutine(Blinking());
    }

    public void cheat()
    {
        invincible = true;
    }
    void Update()
    {
        if (health > 0)
        {
            if (health < healthBefore)
            {
                if (invincible)
                    health = healthBefore;
                else
                {
                    GlobalVariables.global.rewindTime = true;
                    invincible = true;
                }
            }
            healthBefore = health;
            if (invincible && !invincibleStarted)
            {
                Debug.Log("now invincible");
                StartCoroutine(Invincible());
                invincibleCounter = 200;
            }
            if (invincibleCounter > 0)
            {
                invincibleCounter -= 1;
                if (invincibleCounter == 0)
                    invincible = false;
            }
            else
            {
                invincible = false;
            }
            float direction;
            if (GlobalVariables.global.joystick.gameObject.activeSelf)
                direction = GlobalVariables.global.joystick.GetInputDirection().x;
            else
                direction = Input.GetAxis("Horizontal");
            if (direction != 0)
            {
                m_Animator.SetBool("walk", true);
            }
            else
            {
                m_Animator.SetBool("walk", false);
            }
            //if(direction != 0) Debug.Log(direction);
            if (!GlobalVariables.global.busy)
            {
                if (direction < 0)
                {
                    transform.localScale = scaleToUse;
                    transform.Find("body").gameObject.SetActive(false);
                    flipped = true;
                }
                else if (direction > 0)
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
                    if (isJumping && allowJump)
                    {
                        moveDirection.y = jumpSpeed;
                        isJumping = false;
                        m_Animator.SetBool("jump", true);
                    } 
                    else
                    {
                        m_Animator.SetBool("jump", false);
                    }
                }
            }
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            //don't get stuck out of bounds
            if (transform.position.z != zPosition)
            {
                moveDirection.z = (zPosition - transform.position.z);
            }
            if (characterController.enabled)
                characterController.Move(moveDirection * Time.deltaTime);
        }
        else if (!dying)
        {
            StartCoroutine(Death());
        }
    }
    public void jump()
    {
        if (!GlobalVariables.global.busy)
        {
            isJumping = true;
            if (isJumping && allowJump)
            {
                moveDirection.y = jumpSpeed;
                isJumping = false;
                m_Animator.SetBool("jump", true);
            }
            else
            {
                m_Animator.SetBool("jump", false);
            }
        }
    }
    IEnumerator Invincible()
    {
        invincibleStarted = true;
        while (invincible)
        {
            yield return new WaitForSeconds(0.15f);
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = Color.clear;
            }
            yield return new WaitForSeconds(0.15f);
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].name != "eyes")
                    sprites[i].color = Color.gray;
                else sprites[i].color = Color.white; //remove this and his eyes will be blinking during invincibility
            }
        }
        for (int i = 0; i < sprites.Length; i++)
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
        //Debug.Log(col.gameObject.name);
        if (col.transform.tag == "Enemy") //generic enemy, not for long term use
        {
            BaseEnemy enemy = col.gameObject.GetComponent<BaseEnemy>();
            if (!invincible)
            {
                health -= enemy.damage;
                enemy.onDamage.Invoke();
            }
        }
    }
    IEnumerator Blinking()
    {
        while (true)
        {
            eyes.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            eyes.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
        }
    }
}