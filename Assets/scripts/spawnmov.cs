using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmov : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(speed, 0f);
    }
}
