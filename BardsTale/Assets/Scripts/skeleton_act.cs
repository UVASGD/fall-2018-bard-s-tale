using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_act : MonoBehaviour {

    // should be self-explanatory
    public static bool is_attacking;
    public static bool took_damage;

    // 0 is up, 1 is down, 2 is left, 3 is right
    public static int move_direction;

    // valuable skelly boy data
    int health;
    public float movespeed;

    int recoil_cooldown;
    int max_recoil_cooldown = 4;

	// Use this for initialization
	void Start () {
        is_attacking = false;
        took_damage = false;
        move_direction = 0;

        health = 5;

        recoil_cooldown = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // skeleton will attack if it is within 0.05 of the hero
        if (recoil_cooldown > 0)
        {
            recoil_cooldown--;
        }
        else if (distance_to_hero() <= 0.05f)
        {
            is_attacking = true;
            took_damage = false;
            static_information.hero.GetComponent<hero_act>().takeDamage();

            move_direction = -1;
        }
        else
        {
            is_attacking = false;
            took_damage = false;
            Vector2 new_position = new Vector2(transform.position.x, transform.position.y);
            int x_direct = 0, y_direct= 0; float small_float_value = 0.05f;
            // Skeleton is right of hero
            if (transform.position.x - static_information.hero.transform.position.x > small_float_value)
            {
                x_direct = -1;
                move_direction = 2;
            }
            // Skeleton is left of hero
            else if (transform.position.x - static_information.hero.transform.position.x < (-1 * small_float_value))
            {
                x_direct = 1;
                move_direction = 3;
            }

            // Skeleton is up of hero
            if (transform.position.y - static_information.hero.transform.position.y > small_float_value)
            {
                y_direct = -1;
                move_direction = 0;
            }
            // Skeleton is down of hero
            else if (transform.position.y - static_information.hero.transform.position.y < (-1 * small_float_value))
            {
                y_direct = 1;
                move_direction = 1;
            }



            // if neither x_diff nor y_diff are true, we won't change new_position.
            if (x_direct != 0)
            {
                new_position.x += (x_direct) * (movespeed / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
            }
            if (y_direct != 0)
            {
                new_position.y += (y_direct) * (movespeed / Mathf.Sqrt(Mathf.Abs(x_direct) + Mathf.Abs(y_direct)));
            }
            
            transform.position = new_position;
        }
		
	}

    float distance_to_hero()
    {
        return Mathf.Sqrt(Mathf.Pow(static_information.hero.transform.position.x - transform.position.x, 2) + Mathf.Pow(static_information.hero.transform.position.y - transform.position.y, 2));
    }

    public void takeDamage()
    {
        took_damage = true;
        recoil_cooldown = max_recoil_cooldown * GetComponent<skeleton_animation_script>().cooldown_max;
        health--;
        Debug.Log("Skeleton took damage! Health is: " + health);
    }
}
