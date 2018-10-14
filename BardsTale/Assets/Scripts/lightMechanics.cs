using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lightMechanics : MonoBehavior
{
    //how many seconds the first-level light spell adds to the light-level in the room
    //A.K.A.: After how many seconds should you cast light to maintain the light-level
    public static float light_spell_increment = 5;

    void Start()
    {

    }

    void Update()
    {
        if (!static_information.isPauased)
        {
            float timePassed = time.deltaTime();

            for (int i = 0; i < static_information.room_light_levels.length; i++)
            {
                if (static_information.room_light_levels[i] < timePassed)
                {
                    static_information.room_light_levels[i] = 0;
                }
                else
                {
                    static_information.room_light_levels[i] -= timePassed;
                }
            }
        }
    }

    //add light adds light to the room by the light_spell_incrememt multiplied by the multiplier
    //(multiplier = 1 for first level, and we can figure out the rest later)
    public void addLight(float multiplier)
    {
        static_information.room_light_levels[static_information.bard_room_location] += light_spell_increment * multiplier;

        if(static_information.room_light_levels[static_information.bard_room_location] > static_information.max_light_level)
        {
            static_information.room_light_levels[static_information.bard_room_location] = static_information.max_light_level;
        }
        //call animations here
    }
}
