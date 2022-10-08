using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    private float length;
    private float startpos;
    public Camera cam;
    [SerializeField] private float paralaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (cam.transform.position.x * paralaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        float temp = (cam.transform.position.x * (1 -paralaxEffect));

        if (temp > startpos + length)
        {
            startpos += length;
        }
        
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
