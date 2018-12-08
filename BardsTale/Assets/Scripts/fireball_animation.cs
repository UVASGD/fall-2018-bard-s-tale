using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_animation : MonoBehaviour
{

    public string path;

    public float speed = 0.05f;

    SpriteRenderer r;
    Sprite[] spritesList;
    Vector2 my_dimensions;

    int animation_offset;
    int frame_offset;
    int animation_length;

    int cooldown;
    int cooldown_max = 4;

    int direction;
    bool exploding;

    /* THE SPRITES LIST IS AS FOLLOWS:
     * First 6 images: Fireball moving down (direction = 2, exploding = false)
     * Next 6 images: Fireball moving right (direction = 1 or 3, exploding = false)
     * Next 6 images: Fireball moving up (direction = 0, exploding = false)
     * Final 6 images: Fireball exploding (exploding = true)
     */


    // Use this for initialization
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        r.enabled = false;
        spritesList = Resources.LoadAll<Sprite>(path);

        my_dimensions = new Vector2(0.023f, 0.12f);

        exploding = false;

        animation_offset = 0;
        frame_offset = 0;
        animation_length = 6;

        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (r.enabled)
        {
            if (cooldown > 0)
            {
                cooldown--;
                return;
            }

            cooldown = cooldown_max;
            if (exploding)
            {
                if (frame_offset % animation_length != 5)
                {
                    r.sprite = spritesList[animation_offset + (++frame_offset % animation_length)];
                }
                else
                {
                    Start();
                }
            }
            else
            {
                Vector2 new_position = transform.position;
                switch(direction)
                {
                    case 0:
                        new_position.y += speed;
                        animation_offset = 12;
                        break;
                    case 2:
                        new_position.y -= speed;
                        animation_offset = 0;
                        break;
                    case 1:
                        new_position.x -= speed;
                        r.flipX = true;
                        animation_offset = 6;
                        break;
                    case 3:
                        new_position.x += speed;
                        r.flipX = false;
                        animation_offset = 6;
                        break;
                }
                transform.position = new_position;

                detect_collision();

                r.sprite = spritesList[animation_offset + (++frame_offset % animation_length)];
            }
        }

    }

    void detect_collision()
    {
        foreach (GameObject g in static_information.enemies)
        {
            Vector2 dimensions;
            if (g.GetComponent<skeleton_act>() != null)
            {
                dimensions = g.GetComponent<skeleton_act>().dimensions;
            }
            else
            {
                dimensions = g.GetComponent<Zombie_act>().dimensions;
            }
            Vector2 differences = new Vector2(dimensions.x + my_dimensions.x, dimensions.y + my_dimensions.y);
            if (Mathf.Abs(g.transform.position.x - transform.position.x) < differences.x)
            {
                if (Mathf.Abs(g.transform.position.y - transform.position.y) < differences.y)
                {
                    //Debug.Log("Explode!");

                    exploding = true;
                    animation_offset = 18;
                    if (g.GetComponent<skeleton_act>() != null)
                    { g.GetComponent<skeleton_act>().takeDamage(); }
                    else { g.GetComponent<Zombie_act>().takeDamage(); }
                }
            }
        }

        if (static_information.is_in_bounds(transform.position) == false)
        {
            //Debug.Log("Explode!");

            exploding = true;
            animation_offset = 18;
        }

    }

    public void castFireball()
    {
        r.enabled = true;
        direction = static_information.hero.GetComponent<hero_move>().moveDir;
        switch (direction)
        {
            case 0: // move up
                animation_offset = 12;
                break;
            case 1: // move left
                animation_offset = 6;
                r.flipX = true;
                break;
            case 2: // move down
                animation_offset = 0;
                break;
            case 3: // move right
                animation_offset = 6;
                r.flipX = false;
                break;
            default: // move... wait
                Debug.Log("Ding dong! Something's wrong!");
                break;
        }
    }
}
