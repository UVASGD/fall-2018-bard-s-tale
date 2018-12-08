using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_animation_script : MonoBehaviour {

    public string path;
    public int[] animationLengths;
    public int[] animationStarts;

    private int animationLength;
    private int animationStart;
    private int animation_type_offset;
    private int animation_type_offset_incrementer;

    private Sprite[] spritesList;
    private int counter = 0;
    private int cooldown;
    public int cooldown_max = 4;
    public bool dying = false;

	// Use this for initialization
	void Start ()
    {
        spritesList = Resources.LoadAll<Sprite>(path);
        // Debug.Log(spritesList.Length);

        animationLength = animationLengths[0];
        animationStart = animationStarts[0];
        animation_type_offset = 0;

        // last animation + start of last animation
        animation_type_offset_incrementer = animationStarts[animationStarts.Length - 1] + animationLengths[animationLengths.Length - 1];

        cooldown = cooldown_max;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (dying && animationStart < 43)
        {
            animationStart = 43;
            animationLength = 6;
            counter = 0;
            animation_type_offset = 0;
        }

        if (!dying && GetComponent<skeleton_act>().is_dead)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            return;
        }

        if (!dying)
        {
            // CHANGE FRAME
            if (GetComponent<skeleton_act>().move_direction == -1)
            {
                animationLength = animationLengths[0]; // 2
                animationStart = animationStarts[0]; // 0
            }
            if (GetComponent<skeleton_act>().move_direction != -1)
            {
                animationLength = animationLengths[1]; // 3
                animationStart = animationStarts[1]; // 2
            }
            if (skeleton_act.is_attacking)
            {
                animationLength = animationLengths[2]; // 5
                animationStart = animationStarts[2]; // 5
            }
            if (skeleton_act.took_damage)
            {
                animationLength = animationLengths[3]; // 4
                animationStart = animationStarts[3]; // 10
            }

            if (GetComponent<skeleton_act>().move_direction == 2)
            {
                animation_type_offset = 2 * animation_type_offset_incrementer;
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            if (GetComponent<skeleton_act>().move_direction == 3)
            {
                animation_type_offset = 2 * animation_type_offset_incrementer;
                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            if (GetComponent<skeleton_act>().move_direction == 1)
            {
                animation_type_offset = animation_type_offset_incrementer;
            }
            if (GetComponent<skeleton_act>().move_direction == 0)
            {
                animation_type_offset = 0;
            }
        }


        if (cooldown <= 0)
        {
            // INCREMENT FRAME
            int frameNumber = animation_type_offset + animationStart + ((counter++) % animationLength);
            //Debug.Log("frame " + frameNumber);
            GetComponent<SpriteRenderer>().sprite = spritesList[frameNumber % spritesList.Length];
            cooldown = cooldown_max;
            if (counter % animationLength == 0)
            {
                dying = false;
            }
        }
        else
        {
            cooldown--;
        }


    }
}
