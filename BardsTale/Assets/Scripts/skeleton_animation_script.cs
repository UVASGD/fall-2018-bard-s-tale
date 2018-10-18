using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_animation_script : MonoBehaviour {

    public string path;
    public int animationLength;
    public int animationStart;

    private Sprite[] spritesList;
    private int counter = 0;
    private int cooldown = 4;

	// Use this for initialization
	void Start ()
    {
        spritesList = Resources.LoadAll<Sprite>(path);
        Debug.Log(spritesList.Length);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // CHANGE FRAME
        if (Input.GetKeyDown(KeyCode.I))
        {
            animationLength = 2;
            animationStart = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animationLength = 3;
            animationStart = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animationLength = 5;
            animationStart = 5;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animationLength = 4;
            animationStart = 10;
        }


        if (cooldown <= 0)
        {
            // INCREMENT FRAME
            int frameNumber = animationStart + (counter++) % animationLength;
            Debug.Log("frame " + frameNumber);
            GetComponent<SpriteRenderer>().sprite = spritesList[frameNumber];
            cooldown = 16;
        }
        else
        {
            cooldown--;
        }


    }
}
