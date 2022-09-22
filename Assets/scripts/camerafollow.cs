using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.transform.position.y, 0f, 100f), transform.position.z);

        /*if (transform.position.y <= -0.91)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            Debug.Log("camara is not locked on the y");
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            Debug.Log("camara is locked");
        }*/
    }
}
