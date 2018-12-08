using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_act : MonoBehaviour {

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
    public float movespeed;
    public bool is_dead;

    int recoil_cooldown;
    int max_recoil_cooldown = 4;

    int damage_cooldown;

    int move_cycle = 16;
    int move_cycle_current = 0;
    float[] movespeeds = new float[] {
        0.5f, 0.5f, 0.75f, 1.5f, 1.5f, 0.75f, 0.5f, 0.5f,
        0.5f, 0.5f, 0.75f, 1.5f, 1.5f, 0.75f, 0.5f, 0.5f
    };


    // Use this for initialization
    void Start() {
        is_attacking = false;
        took_damage = false;
        move_direction = 0;
        
        recoil_cooldown = 0;
        damage_cooldown = 0;
    }

    // Update is called once per frame
    void Update() {
        if (static_information.isPaused == false)
        {
            if (!is_dead)
            {
                // zombie will attack if it is within 0.05 of the hero
                if (recoil_cooldown > 0)
                {
                    recoil_cooldown--;
                }
                else if (distance_to_hero() <= 0.1f)
                {
                    is_attacking = true;
                    took_damage = false;
                    if (damage_cooldown > 0)
                    {
                        damage_cooldown--;
                    }
                    else
                    {
                        static_information.hero.GetComponent<hero_act>().takeDamage();
                        damage_cooldown = 5 * GetComponent<skeleton_animation_script>().cooldown_max;
                    }

                    move_direction = -1;
                }
                else
                {
                    is_attacking = false;
                    took_damage = false;
                    if (static_information.which_room_am_I_in(transform.position.x, transform.position.y) == static_information.room_index)
                    {
                        Vector2 new_position = new Vector2(transform.position.x, transform.position.y);
                        int x_direct = 0, y_direct = 0; float small_float_value = 0.05f;
                        // Zombie is right of hero
                        if (transform.position.x - static_information.hero.transform.position.x > small_float_value)
                        {
                            x_direct = -1;
                            move_direction = 2;
                        }
                        // Zombie is left of hero
                        else if (transform.position.x - static_information.hero.transform.position.x < (-1 * small_float_value))
                        {
                            x_direct = 1;
                            move_direction = 3;
                        }

                        // Zombie is up of hero
                        if (transform.position.y - static_information.hero.transform.position.y > small_float_value)
                        {
                            y_direct = -1;
                            move_direction = 0;
                        }
                        // Zombie is down of hero
                        else if (transform.position.y - static_information.hero.transform.position.y < (-1 * small_float_value))
                        {
                            y_direct = 1;
                            move_direction = 1;
                        }



                        // if neither x_diff nor y_diff are true, we won't change new_position.
                        if (x_direct != 0)
                        {
                            new_position.x += (x_direct) * ((movespeed * movespeeds[++move_cycle_current % move_cycle]) / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
                        }
                        if (y_direct != 0)
                        {
                            new_position.y += (y_direct) * ((movespeed * movespeeds[++move_cycle_current % move_cycle]) / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
                        }

                        if (static_information.is_in_bounds(new_position))
                        { transform.position = new_position; }
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
            // Debug.Log("Zombie took damage! Health is: " + health);
        }
    }
}
