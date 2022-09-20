using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    Rigidbody2D layerobject;
    public GameObject character;

    private float stamina = 100f;
    public float MaxStamina = 100.0f;
    private float speed = 10f;
    public float basespeed = 10f;
    public float sprspeed = 20f;
    public float jmpforce = 100f;
    float turnSpeed = 500;
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
    bool isrot = false;
    bool isonhead = false;
    bool isonside = false;
    bool doublejump = false;
    public bool dead = false;
    public Stambar stambar;
    Vector3 basesize;
    Vector3 slidesize;
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
        basesize = character.transform.localScale;
        slidesize = new Vector3(character.transform.localScale.x, character.transform.localScale.y / 2f, character.transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        float movmentValueX = Input.GetAxis("Horizontal") * speed;

        layerobject.velocity = new Vector2(movmentValueX, layerobject.velocity.y);
        isgrounded = Physics2D.OverlapCircle(groundchecker.transform.position, 0.1f, whatisground);
        isonhead = Physics2D.OverlapCircle(headchecker.transform.position, 0.1f, whatisground);
        isonside = Physics2D.OverlapCircle(sidechecker_1.transform.position, 0.1f, whatisground) || Physics2D.OverlapCircle(sidechecker_2.transform.position, 0.1f, whatisground);
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true && issliding == false && doublejump == false)
        {
            layerobject.AddForce(new Vector2(0f, jmpforce));
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



        if (Input.GetKey(KeyCode.S) && isgrounded == false && isrot == false && issliding == false && isonside == false)
        {
            isrot = true;





        }
        if (isrot)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 1f), turnSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z >= 359f)
            {
                isrot = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (isrot && isonhead || isrot && isonside)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.S) && issprint && stamina > 0f && targetTime <= slidingTime && isrot == false)//prototype slide get rid of transform when adding animation for a hitbox change instead
        {

            issliding = true;
        }
        else
        {
            // if player stopped holding the button replenish the sliding time
            issliding = false;
            targetTime =targetTime + 0.05f;
            transform.localScale = basesize;
        }

        if (issliding)
        {
            layerobject.AddForce(slidingSpeed);
            targetTime = targetTime - 0.001f;
            transform.localScale = slidesize;
        }
        
        if (targetTime < 0f)
        {
            transform.localScale = basesize;
            
        }
        if (targetTime > slidingTime)
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
    }
}
