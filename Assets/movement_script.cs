using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_script : MonoBehaviour
{
    Rigidbody2D layerobject;

    public float stamina = 100f;
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
    bool issprint = false;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina < 0f)
        {
            issprint = true;
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina >= 0f)
        {
            issprint = false;
            
        }

        if (issprint == true)
        {
            speed = sprspeed;
            
        }
        else if (issprint == false)
        {
            speed = basespeed;
            
        }
        while (issprint == true)
        {
            stamina = stamina - 1f;
        }

        while (issprint == false)
        {
            stamina = stamina + 1f;
        }
        Debug.Log(stamina);
    }
}
