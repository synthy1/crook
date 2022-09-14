using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Transform Playerpos;
    public Text scoretext;

    // Update is called once per frame
    void Update()
    {
        scoretext.text = Playerpos.position.x.ToString("0");
    }
}
