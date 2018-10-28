using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bard_animation_script : MonoBehaviour {

    public string path;
    public int[] animationStarts;
    public int[] animationLengths;

    private int animationStart;
    private int animationLength;

    private Sprite[] spritesList;
    private int counter;
    public int cooldown;
    private int cooldown_max;

    // Use this for initialization
    void Start ()
    {
        spritesList = Resources.LoadAll<Sprite>(path);
        Debug.Log(spritesList.Length);

        animationLength = animationLengths[0];
        animationStart = animationStarts[0];

        cooldown_max = cooldown;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // CHANGE FRAME
        if (Input.GetKeyDown(static_information.controls[0]))
        {
            animationLength = animationLengths[0]; // 4
            animationStart = animationStarts[0]; // 4
        }
        if (Input.GetKeyDown(static_information.controls[2]))
        {
            animationLength = animationLengths[1]; // 4
            animationStart = animationStarts[1]; // 4
        }
        if (Input.GetKeyDown(static_information.controls[1]))
        {
            animationLength = animationLengths[2]; // 4
            animationStart = animationStarts[2]; // 4
        }
        if (Input.GetKeyDown(static_information.controls[3]))
        {
            animationLength = animationLengths[3]; // 4
            animationStart = animationStarts[3]; // 4
        }


        if (cooldown <= 0)
        {
            // INCREMENT FRAME
            int frameNumber = animationStart + (counter++) % animationLength;
            //Debug.Log("frame " + frameNumber);
            GetComponent<SpriteRenderer>().sprite = spritesList[frameNumber];
            cooldown = cooldown_max;
        }
        else
        {
            cooldown--;
        }
    }
}
