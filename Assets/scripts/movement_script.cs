using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class movement_script : MonoBehaviour
{
    Rigidbody2D layerobject;

    public float stamina = 100f;
    public float MaxStamina = 100.0f;
    public float speed = 10f;
    public float basespeed = 10f;
    public float sprspeed = 20f;
    public float jmpforce = 100f;
    public GameObject groundchecker;
    public LayerMask whatisground;
    public float jumpf = 100f;
    bool isJumping = false;
    bool isgrounded = false;
    bool issliding = false;
    
    private const float StaminaDecreasePerFrame = 50.0f;
    private const float StaminaIncreasePerFrame = 30.0f;
    private const float StaminaTimeToRegen = 3.0f;
    private float StaminaRegenTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        layerobject = GetComponent<Rigidbody2D>();

       
    }

    // Update is called once per frame
    void Update()
    {
        float movmentValueX = Input.GetAxis("Horizontal") * speed;

        layerobject.velocity = new Vector2(movmentValueX, layerobject.velocity.y);
        isgrounded = Physics2D.OverlapCircle(groundchecker.transform.position, 0.1f, whatisground);

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true)
        {
            layerobject.AddForce(new Vector2(0f, jumpf));
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
        Debug.Log(stamina);
    }


}
