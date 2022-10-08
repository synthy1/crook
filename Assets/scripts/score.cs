using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Transform Playerpos;
    public Text scoretext;
    public Character_controller player;

    // Update is called once per frame
    void Update()
    {
        if (player.dead== false) 
        scoretext.text = (Playerpos.position.x / 10f).ToString("0");
    }
}
