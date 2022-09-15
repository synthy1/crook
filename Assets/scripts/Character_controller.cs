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
    public GameObject groundchecker;
    public GameObject Headchecker;
    public GameObject sidechecker;
    public GameObject sidechecker_2;
    public LayerMask whatisground;
    bool isJumping = false;
    bool isgrounded = false;
    bool issliding = false;
    bool runagain = false;
    bool isrot = false;
    bool isonhead = false;
    bool isonside = false;
    bool gamestart = false;
    public bool dead = false;
    public Stambar stambar;
    
    
    private const float StaminaDecreasePerFrame = 50.0f;
    private const float StaminaIncreasePerFrame = 30.0f;
    private const float StaminaTimeToRegen = 3.0f;
    private float StaminaRegenTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        layerobject = GetComponent<Rigidbody2D>();
        float startrot = character.transform.localRotation.z;
        stambar.setmaxstam(MaxStamina);
        speed = basespeed;
        gamestart = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        float movmentValueX = Input.GetAxis("Horizontal") * speed;
        

        layerobject.velocity = new Vector2(movmentValueX, layerobject.velocity.y);
        isgrounded = Physics2D.OverlapCircle(groundchecker.transform.position, 0.1f, whatisground);//checking if grounded
        isonhead = Physics2D.OverlapCircle(Headchecker.transform.position, 0.1f, whatisground);//checking if on head
        isonside = Physics2D.OverlapCircle(sidechecker.transform.position, 0.1f, whatisground) || Physics2D.OverlapCircle(sidechecker_2.transform.position, 0.1f, whatisground); ;//checking if on either side

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true)
        {
            layerobject.AddForce(new Vector2(0f, jmpforce));
        }

        bool issprint = Input.GetKey(KeyCode.LeftShift);//sprint + stam function

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

        
        
        if (Input.GetKey(KeyCode.S) && isgrounded == false && isrot == false)//backflip function
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
        
        if (isonhead && isgrounded == false && isrot || isonside && isgrounded == false && isrot) //die if backflipping and dont land on ur feet
        {
            Destroy(gameObject);
        }
        

    }

 
}
