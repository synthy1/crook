using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_script : MonoBehaviour
{
    Rigidbody2D layerobject;
   
    public float speed = 100f;
    public GameObject groundchecker;
    public LayerMask whatisground;
    public float jumpf = 100f;
    bool isJumping = true;
    bool isgrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        layerobject = GetComponent<Rigidbody2D>();

       
    }

    // Update is called once per frame
    void Update()
    {
        float movmentValueX = Input.GetAxis("Horizontal") * speed*Time.deltaTime;

        layerobject.velocity = new Vector2(movmentValueX, layerobject.velocity.y);
        isgrounded = Physics2D.OverlapCircle(groundchecker.transform.position, 1.0f, whatisground);

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true)
        {
            layerobject.AddForce(new Vector2(0f, 100f));
        }


    }
}
