using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    Rigidbody2D layerobject;
    public GameObject character;
    public Animator anim;

    private float stamina = 100f;
    public float MaxStamina = 100.0f;
    private float speed = 10f;
    public float basespeed = 10f;
    public float sprspeed = 20f;
    public float jmpforce = 100f;
    float turnSpeed = 500f;
    float targetTime = 0f;
    float slidingTime = 1f;// how long can player slide
    public GameObject groundchecker;
    public GameObject headchecker;
    public GameObject sidechecker_1;
    public GameObject sidechecker_2;
    public LayerMask whatisground;
    bool isJumping = false;
    bool isgrounded = false;
    bool issliding = false;
    public bool isrot = false;
    bool isonhead = false;
    bool isonside = false;
    bool doublejump = false;
    public bool dead = false;
    public Stambar stambar;

    
    public Vector2 slidingSpeed = new Vector2(500f, 0);
    public Vector2 DJ_Hight = new Vector2(0f, 0.1f);
    private const float StaminaDecreasePerFrame = 50.0f;
    private const float StaminaIncreasePerFrame = 50.0f;
    private const float StaminaTimeToRegen = 2.0f;
    private float StaminaRegenTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        layerobject = GetComponent<Rigidbody2D>();
        float startrot = character.transform.localRotation.z;
        stambar.setmaxstam(MaxStamina);
        speed = basespeed;
        

    }

    // Update is called once per frame
    void Update()
    {
        float movmentValueX = speed;

        layerobject.velocity = new Vector2(movmentValueX, layerobject.velocity.y);
        isgrounded = Physics2D.OverlapCircle(groundchecker.transform.position, 0.5f, whatisground);
        isonhead = Physics2D.OverlapCircle(headchecker.transform.position, 0.05f, whatisground);
        isonside = Physics2D.OverlapCircle(sidechecker_1.transform.position, 0.1f, whatisground) || Physics2D.OverlapCircle(sidechecker_2.transform.position, 0.1f, whatisground);
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true && issliding == false && doublejump == false)
        {
            layerobject.AddForce(new Vector2(0f, jmpforce));
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }

        bool issprint = Input.GetKey(KeyCode.LeftShift);

        if (issprint)
        {
            stamina = Mathf.Clamp(stamina - (StaminaDecreasePerFrame * Time.deltaTime), 0.0f, MaxStamina);
            StaminaRegenTimer = 0.0f;
            speed = sprspeed;
        }
        else if (stamina < MaxStamina)
        {
            speed = basespeed;
            if (StaminaRegenTimer >= StaminaTimeToRegen)
                stamina = Mathf.Clamp(stamina + (StaminaIncreasePerFrame * Time.deltaTime), 0.0f, MaxStamina);
            else
                StaminaRegenTimer += Time.deltaTime;
        }
        if (stamina == 0f)
        {
            speed = basespeed;
        }
        stambar.setstam(stamina);


       


        if (Input.GetKey(KeyCode.S) && issprint == true && targetTime <= 0f && isrot == false)//prototype slide get rid of transform when adding animation for a hitbox change instead
        {

            issliding = true;
        }
        else
        {
            // if player stopped holding the button replenish the sliding time
            issliding = false;
            targetTime -= 0.05f;
        }

        if (issliding)
        {
            
            layerobject.AddForce(slidingSpeed);
            targetTime = targetTime + 0.002f;
            
        }
        
        if (targetTime < 0f)
        {
            
            issliding = false;
            targetTime = 0f;
            
        }
        if (targetTime > slidingTime && issliding == true)
        {
            targetTime = slidingTime;
        }
        
        if (issliding && Input.GetKey(KeyCode.Space) && isgrounded)
        {
            doublejump = true;
        }
        else
        {
            doublejump = false;
        }

        if (doublejump)
        {
            layerobject.AddForce(DJ_Hight);
            issliding = false;
            doublejump = false;
        }
        anim.SetBool("isgrounded", isgrounded);
        anim.SetBool("issliding", issliding);

        Debug.Log(transform.rotation.eulerAngles.z);
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S) && isgrounded == false && isrot == false && isonside == false)
        {
            isrot = true;

        }

        if (isrot)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 5f), turnSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z >= 300f)
            {
                isrot = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (isrot && isonhead || isrot && isonside)
        {
            //kill player
        }
    }
}
