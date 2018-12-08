using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_animation_script : MonoBehaviour {

    public string path;

    SpriteRenderer r;
    Sprite[] spritesList;

    int animation_offset;
    int animation_length;
    public int frame_offset;

    int cooldown;
    public int cooldown_max = 4;

    public bool spitting;
    public bool dying;

    Vector2[] random_spawn_areas = new Vector2[] { new Vector2(8.020554f, 6.153f), new Vector2(8.020554f, 8.059f), new Vector2(11.155f, 8.059f), new Vector2(11.14f, 6.16f) };

	// Use this for initialization
	void Start () {
        spritesList = Resources.LoadAll<Sprite>(path);
        r = GetComponent<SpriteRenderer>();

        animation_offset = 0;
        animation_length = 0;
        frame_offset = 0;

        cooldown = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (static_information.isPaused)
        {
            return;
        }

        if (cooldown > 0)
        {
            cooldown--;
            return;
        }

        if (r.enabled == false)
        {
            return;
        }

        cooldown = cooldown_max;

		if (dying) // play dying animation
        {
            animation_offset = (3 * 14) + (3 * 8);
            animation_length = 27;
            r.sprite = spritesList[animation_offset + (++frame_offset % animation_length)];

            if (frame_offset % animation_length == 0)
            {
                frame_offset = 0;
                r.enabled = false;
                frame_offset = 0;
                dying = false;
                return;
            }
        }

        if (spitting) // play spitting animation
        {
            animation_length = 14;
            if(GetComponent<skeleton_act>().move_direction == 1) // moving up
            {
                animation_offset = (2 * 14);
            }
            else if(GetComponent<skeleton_act>().move_direction == 0) // moving down
            {
                animation_offset = 0;
            }
            else // moving laterally
            {
                animation_offset = 14;
                r.flipX = (GetComponent<skeleton_act>().move_direction == 3);
            }
            r.sprite = spritesList[animation_offset + (++frame_offset % animation_length)];
            if (frame_offset % animation_length == 0)
            {
                frame_offset = 0;
                // call summon skeleton
                spit();
                // turn off spit
                spitting = false;
                return;
            }
        }

        if (!dying && !spitting) // call moving animation (repeats)
        {
            //Debug.Log("Boss is moving");
            animation_length = 8;
            if (GetComponent<skeleton_act>().move_direction == 1)
            {
                animation_offset = (3 * 14) + 16;
            }
            else if (GetComponent<skeleton_act>().move_direction == 0)
            {
                animation_offset = 3 * 14;
            }
            else
            {
                animation_offset = (3 * 14) + 8;
                r.flipX = (GetComponent<skeleton_act>().move_direction == 3);
            }
            r.sprite = spritesList[animation_offset + (++frame_offset % animation_length)];
        }
	}

    void spit()
    {
        foreach (GameObject g in static_information.enemies)
        {
            Debug.Log(g.name + ": " + static_information.which_room_am_I_in(g.transform.position.x, g.transform.position.y));
            if (static_information.which_room_am_I_in(g.transform.position.x, g.transform.position.y) == -1)
            {
                // enemy is not in any room
                Debug.Log("found an unroomed enemy.");
                // pick a random corner
                int corner_num = Random.Range(0, 4); //0, 1, 2, 3
                g.transform.position = random_spawn_areas[corner_num];
                return;
            }
        }
    }
}
