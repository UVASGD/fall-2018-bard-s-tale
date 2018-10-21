using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public Event 

public class lightMechanics : MonoBehaviour
{
    //how many seconds the first-level light spell adds to the light-level in the room
    //A.K.A.: After how many seconds should you cast light to maintain the light-level
    public static float light_spell_increment = 5;
    SpriteRenderer sr;

    public float startLevel = 0;
    public float currentLevel = 0;
    public bool playerInside = false;


    void Start()
    {
        // player.spellcontroller.lightSpell += addLight;
        currentLevel = startLevel;
        sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < static_information.room_light_levels.Length; i++)
        {
            static_information.room_light_levels[i] = 25;
        }
    }

    void Update()
    {
        if (!static_information.isPaused)
        {

            //float timePassed = Time.deltaTime;

            //for (int i = 0; i < static_information.room_light_levels.Length; i++)
            //{
            //    if (static_information.room_light_levels[i] < timePassed)
            //    {
            //        static_information.room_light_levels[i] = 0;
            //    }
            //    else
            //    {
            //        static_information.room_light_levels[i] -= timePassed;
            //    }
            //}
            currentLevel -= Time.deltaTime;
            float opacity = (currentLevel <= static_information.max_light_level)?
                static_information.maximumDarknessOpacity - ((currentLevel / static_information.max_light_level) * static_information.maximumDarknessOpacity) : 0;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, opacity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") playerInside = false;
    }

    //add light adds light to the room by the light_spell_incrememt multiplied by the multiplier
    //(multiplier = 1 for first level, and we can figure out the rest later)
    public void addLight(float multiplier)
    {
        if (!playerInside) return;
        currentLevel += light_spell_increment * multiplier;

        if(currentLevel > static_information.max_light_level)
        {
            currentLevel = static_information.max_light_level;
        }
        //call animations here
    }
}
