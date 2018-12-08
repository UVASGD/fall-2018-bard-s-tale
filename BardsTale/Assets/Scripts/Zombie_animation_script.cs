using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_animation_script : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        spritesList = Resources.LoadAll<Sprite>(path);
        // Debug.Log(spritesList.Length);

        animationLength = animationLengths[0];
        animationStart = animationStarts[0];
        animation_type_offset = 0;

        // last animation + start of last animation
        animation_type_offset_incrementer = 13;

        cooldown = cooldown_max;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<Zombie_act>().is_dead)
        {
            return;
        }

        // this was a nice idea but I'm not sticking with it until it works correctly.
        bool animation_change = true;
        foreach (int i in animationStarts)
        {
            // we can change animations when idling or walking at any time, but I want damage and attack to actually go through.
            if (i == animationStarts[0] || i == animationStarts[1])
            {
                if (counter == (i - 1) + animation_type_offset)
                {
                    animation_change = true;
                }
            }
        }

        if (animation_change)
        {
            // CHANGE FRAME
            if (GetComponent<Zombie_act>().move_direction == -1)
            {
                animationLength = animationLengths[0]; // 2
                animationStart = animationStarts[0]; // 0
            }
            if (GetComponent<Zombie_act>().move_direction != -1)
            {
                animationLength = animationLengths[1]; // 4
                animationStart = animationStarts[1]; // 2
            }
            if (Zombie_act.is_attacking)
            {
                animationLength = animationLengths[2]; // 4
                animationStart = animationStarts[2]; // 6
            }
            if (Zombie_act.took_damage)
            {
                animationLength = animationLengths[3]; // 4
                animationStart = animationStarts[3]; // 10
            }

            if (GetComponent<Zombie_act>().move_direction == 2)
            {
                animation_type_offset = 0;
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            if (GetComponent<Zombie_act>().move_direction == 3)
            {
                animation_type_offset = 0;
                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            if (GetComponent<Zombie_act>().move_direction == 1)
            {
                animation_type_offset = animation_type_offset_incrementer;
            }
            if (GetComponent<Zombie_act>().move_direction == 0)
            {
                animation_type_offset = 0;
            }
        }


        if (cooldown <= 0)
        {
            // INCREMENT FRAME
            int frameNumber = animation_type_offset + animationStart + ((counter++) % animationLength);
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
