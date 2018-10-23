using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lightMechanics : MonoBehaviour
{
    ///<summary>
    ///how many seconds the first-level light spell adds to the light-level in the room
    ///A.K.A.: After how many seconds should you cast light to maintain the light-level
    ///</summary>
    public static float light_spell_increment = 5;
    SpriteRenderer sr;

    /// <summary>
    /// The light level of a room when the room is created
    /// </summary>
    public float startLevel = 0;
    /// <summary>
    /// A number indicating the level of light in the room. It is the same as the number
    /// of seconds it takes for the room to completely dim
    /// </summary>
    private float currentLightLevel = 0;
    public bool playerInside = false;


    void Start()
    {

        currentLightLevel = startLevel;
        sr = GetComponent<SpriteRenderer>();
        print(sr);
        print(sr.color);

        // Subscribe to the Light Spell casting event
        // when the player casts a spell, this room's instance of addLight will be called
        SpellCasting.instance.OnCastLightSpell += addLight;
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

            currentLightLevel -= Time.deltaTime;//decrease time

            if(currentLightLevel < 0)//making sure that light cannot be lower than 0
            {
                currentLightLevel = 0;
            }

            //making sure that light cannot be greater than max_light_level
            float opacity = (currentLightLevel <= static_information.max_light_level)?
                static_information.maximumDarknessOpacity - ((currentLightLevel / static_information.max_light_level) 
                * static_information.maximumDarknessOpacity) : 0;

            //changes the opacity of the spriteRenderer (the black box)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, opacity);
        }
    }

    //collision for room, enter
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") playerInside = true;
    }

    //collision for room, exit
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") playerInside = false;
    }

    /// <summary>
    ///add light adds light to the room by the light_spell_incrememt multiplied by the multiplier
    ///(multiplier = 1 for first level, and we can figure out the rest later)
    ///</summary>
    public void addLight(float multiplier)
    {
        if (!playerInside) return;
        currentLightLevel += light_spell_increment * multiplier;

        if(currentLightLevel > static_information.max_light_level)
        {
            currentLightLevel = static_information.max_light_level;
        }
        //call animations here
    }

    public float getCurrentLightLevel()
    {
        return currentLightLevel;
    }
}
