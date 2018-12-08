using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_boss_act : MonoBehaviour
{

    // should be self-explanatory
    public static bool is_attacking;
    public static bool took_damage;

    // skeleton defaults are x: 0.067, y: 0.167
    // boss defaults are x: unimplemented, y: unimplemented
    public Vector2 dimensions;

    // 0 is up, 1 is down, 2 is left, 3 is right
    public int move_direction;

    // valuable skelly boy data
    int health;
    int maxhealth;
    public float movespeed;
    public bool is_dead;
    public bool is_boss = true;

    int spit_cooldown;
    int max_spit_cooldown = 50;

    int recoil_cooldown;
    int max_recoil_cooldown = 4;

    int damage_cooldown;

    float healing_cooldown;
    float max_healing_cooldown;

    // Use this for initialization
    void Start()
    {
        is_attacking = false;
        took_damage = false;
        move_direction = 0;

        health = 10;
        maxhealth = 10;

        spit_cooldown = 0;
        recoil_cooldown = 0;
        damage_cooldown = 0;
        max_healing_cooldown = 1f;
        healing_cooldown = max_healing_cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (static_information.isPaused == false)
        {
            if (!is_dead)
            {
                // skeleton will attack if it is within 0.05 of the hero
                int roomNumber = static_information.which_room_am_I_in(transform.position.x, transform.position.y);

                if (roomNumber != static_information.room_index)
                {
                    return;
                }

                string light_id = "light_machine (" + roomNumber + ")";
                GameObject light_machine = GameObject.Find(light_id);
                float lightLevel = light_machine.GetComponent<simple_light>().alpha;

                //healing when light levels are 0
                if (lightLevel == static_information.max_light_level - 15.0f)
                {
                    healing_cooldown -= Time.deltaTime;
                    if(healing_cooldown < 0)
                    {
                        healing_cooldown = max_healing_cooldown;
                        health++;
                    }
                }

                if (spit_cooldown > 0)
                {
                    spit_cooldown--;
                }
                else
                {
                    int count = 0;
                    int totalcount = 0;
                    foreach (GameObject g in static_information.enemies)
                    {
                        if (static_information.which_room_am_I_in(g.transform.position.x, g.transform.position.y) == static_information.room_index)
                        {
                            if (g.GetComponent<skeleton_act>().is_dead == false)
                            {
                                count++;
                            }
                            totalcount++;
                        }
                    }
                    //Debug.Log("Count: " + count);
                    if (count < 4 && totalcount < 11)
                    {
                        //Debug.Log("Spitting...");
                        spit();
                        spit_cooldown = max_spit_cooldown;
                    }
                    else
                    {
                        is_attacking = false;
                        took_damage = false;
                        if (static_information.which_room_am_I_in(transform.position.x, transform.position.y) == static_information.room_index)
                        {
                            Vector2 new_position = new Vector2(transform.position.x, transform.position.y);
                            int x_direct = 0, y_direct = 0; float small_float_value = 0.05f;
                            // boss is right of hero
                            if (transform.position.x - static_information.hero.transform.position.x > small_float_value)
                            {
                                x_direct = -1;
                                move_direction = 2;
                            }
                            // boss is left of hero
                            else if (transform.position.x - static_information.hero.transform.position.x < (-1 * small_float_value))
                            {
                                x_direct = 1;
                                move_direction = 3;
                            }

                            // boss is up of hero
                            if (transform.position.y - static_information.hero.transform.position.y > small_float_value)
                            {
                                y_direct = -1;
                                move_direction = 0;
                            }
                            // boss is down of hero
                            else if (transform.position.y - static_information.hero.transform.position.y < (-1 * small_float_value))
                            {
                                y_direct = 1;
                                move_direction = 1;
                            }



                            // if neither x_diff nor y_diff are true, we won't change new_position.
                            if (x_direct != 0)
                            {
                                new_position.x += 0.5f * (x_direct) * (movespeed / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
                            }
                            if (y_direct != 0)
                            {
                                new_position.y += 0.5f * (y_direct) * (movespeed / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
                            }

                            if (static_information.is_in_bounds(new_position))
                            { transform.position = new_position; }
                        }
                    }
                }
            }
        }

    }

    float distance_to_hero()
    {
        return Mathf.Sqrt(Mathf.Pow(static_information.hero.transform.position.x - transform.position.x, 2) + Mathf.Pow(static_information.hero.transform.position.y - transform.position.y, 2));
    }

    public void takeDamage()
    {
        if (!is_dead)
        {
            took_damage = true;
            health--;
            is_dead = (health <= 0);
            if (is_dead)
            {
                GetComponent<boss_animation_script>().frame_offset = 0;
                GetComponent<boss_animation_script>().dying = true;
            }
            // Debug.Log("Skeleton took damage! Health is: " + health);
        }
    }

    void spit()
    {
        GetComponent<boss_animation_script>().spitting = true;
    }
}

