using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JKL; (light)
// KJLJ; (firebolt)
// LK;JK; (health)
// LLLKL;J (super mario)
// ;;;KLLLJ (Bethoveen's 5th)
// KJKJKJLKJ (Sonic)

// space to cast

 /// <summary>
 /// Event for casting the light spell
 /// </summary>
 /// <param name="multiplier"> value in which to add to the current light value of the room </param>
public delegate void SpellLightEvent(float multiplier);
public delegate void SpellFireboltEvent();
public delegate void SpellHealEvent(float healVal);


public class SpellCasting : MonoBehaviour {

    // Singleton reference
    // THERE CAN ONLY BE ONE. EVER.
    public static SpellCasting instance;

    protected string currentString = "";
    protected int startKey = -1;
    protected int[] lightSpellSequence = new int[] { 4, 5, 6, 7 };
    protected int[] boltSpellSequence = new int[] { 5, 4, 6, 4, 7 };
    protected int[] healSpellSequence = new int[] { 6, 5, 7, 4, 5, 7 };
    protected int[] marioSpellSequence = new int[] { 6, 6, 6, 5, 6, 7, 4 };
    protected int[] beethoven5thSpellSequence = new int[] { 7, 7, 7, 5, 6, 6, 6, 4 };
    protected int[] sonicSpellSequence = new int[] { 5, 4, 5, 4, 5, 4, 6, 5, 4 };
    protected int[][] sequencesList;
    protected string[] baseSpellList;

    public AudioSource audio;

    // Spell casting events
    public event SpellLightEvent OnCastLightSpell;
    public event SpellFireboltEvent OnCastFireboltSpell;
    public event SpellHealEvent OnCastHealSpell;

    void Awake() {
        // ensures that there will only ever be one SpellCasting script active
        // in the entire Scene at runtime
        if (instance != null) {
            Destroy(this);
            return;
        }
        
        instance = this;
    }

	// Use this for initialization
	void Start () {

        sequencesList = new int[][] { lightSpellSequence, boltSpellSequence, healSpellSequence,
            marioSpellSequence, beethoven5thSpellSequence, sonicSpellSequence };
        baseSpellList = new string[sequencesList.Length];
        for(int i = 0; i < sequencesList.Length; i++)
        {
            string spellString = "";
            for(int j = 0; j < sequencesList[i].Length; j++)
            {
                spellString += inputHandling.charify(static_information.controls[sequencesList[i][j]].ToString());
            }
            baseSpellList[i] = spellString;
            // Debug.Log(baseSpellList[i]);
        }

        // static_information.lights = GameObject.FindObjectsOfType<lightMechanics>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!static_information.isPaused)
        {
            if (Input.GetKeyDown(static_information.controls[4]))
            {
                currentString += inputHandling.charify(static_information.controls[4].ToString());
                if (startKey == -1)
                {
                    startKey = 4;
                }
                playTone(4, startKey);

                // Debug.Log(currentString);
            }
            else if (Input.GetKeyDown(static_information.controls[5]))
            {
                currentString += inputHandling.charify(static_information.controls[5].ToString());
                if (startKey == -1)
                {
                    startKey = 5;
                }
                playTone(5, startKey);

                // Debug.Log(currentString);
            }
            else if (Input.GetKeyDown(static_information.controls[6]))
            {
                currentString += inputHandling.charify(static_information.controls[6].ToString());
                if (startKey == -1)
                {
                    startKey = 6;
                }
                playTone(6, startKey);

                // Debug.Log(currentString);
            }
            else if (Input.GetKeyDown(static_information.controls[7]))
            {
                currentString += inputHandling.charify(static_information.controls[7].ToString());
                if (startKey == -1)
                {
                    startKey = 7;
                }
                playTone(7, startKey);

                // Debug.Log(currentString);
            }
            else if (Input.GetKeyDown(static_information.controls[8]))
            {
                for (int i = 0; i < baseSpellList.Length; i++)
                {
                    if (currentString.Equals(baseSpellList[i]))
                    {
                        // Debug.Log("Successful Spell");
                        currentString = "";
                        startKey = -1;

                        //call appropriate function
                        switch (i)
                        {
                            case 0: //light spell
                                //Debug.Log("Casting Light!");
                                static_information.hero.GetComponentInChildren<self_spellcast_animation>().castSpell("light");
                                cast_light_spell();
                                break;
                            case 1: //fireball spell
                                //Debug.Log("Casting Fireball!");
                                static_information.has_casted_fireball = true;
                                for (int j = 0; j < static_information.fireballs.Length; j++)
                                {
                                    if (static_information.fireballs[j].GetComponent<SpriteRenderer>().enabled == false)
                                    {
                                        static_information.fireballs[j].transform.position = static_information.hero.transform.position;
                                        static_information.fireballs[j].GetComponent<fireball_animation>().castFireball();
                                        break;
                                    }
                                }
                                static_information.hero.GetComponentInChildren<self_spellcast_animation>().castSpell("fireball");
                                break;
                            case 2: //healing spell
                                //Debug.Log("Casting Heal!");
                                static_information.hero.GetComponentInChildren<self_spellcast_animation>().castSpell("heal");
                                static_information.hero.GetComponent<hero_act>().healDamage();
                                break;
                            case 3: //mario spell
                                break;
                            case 4: //beethoven spell
                                break;
                            case 5: //sonic spell
                                break;
                        }

                        return;
                    }
                }
                // Debug.Log("Spell Failed");
                currentString = "";
                startKey = -1;
            }
        }
    }

    void cast_light_spell()
    {
        int room = static_information.which_room_am_I_in(static_information.hero.transform.position.x, static_information.hero.transform.position.y);
        string light_id = "light_machine (" + room + ")";
        GameObject light_machine = GameObject.Find(light_id);
        light_machine.GetComponent<simple_light>().addLight();
    }

    //plays the note
    protected void playTone(int key, int start)
    {
        // Debug.Log("Played: " + key + ", " + start);

        float note = -1;
        var transpose = -4;

        switch (key)
        {
            case 4:
                switch (start)
                {
                    case 4:
                        note = 7;
                        break;
                    case 5:
                        note = 9;
                        break;
                    case 6:
                        note = 7;
                        break;
                    case 7:
                        note = 2;
                        break;
                }
                break;
            case 5:
                switch (start)
                {
                    case 4:
                        note = 9;
                        break;
                    case 5:
                        note = 11;
                        break;
                    case 6:
                        note = 12;
                        break;
                    case 7:
                        note = 4;
                        break;
                }
                break;
            case 6:
                switch (start)
                {
                    case 4:
                        note = 11;
                        break;
                    case 5:
                        note = 12;
                        break;
                    case 6:
                        note = 16;
                        break;
                    case 7:
                        note = 5;
                        break;
                }
                break;
            case 7:
                switch (start)
                {
                    case 4:
                        note = 11.75f;
                        break;
                    case 5:
                        note = 14;
                        break;
                    case 6:
                        note = 19;
                        break;
                    case 7:
                        note = 7;
                        break;
                }
                break;
        }

        //if (Input.GetKeyDown("1")) note = 0;  // C
        //if (Input.GetKeyDown("2")) note = 2;  // D
        //if (Input.GetKeyDown("3")) note = 4;  // E
        //if (Input.GetKeyDown("4")) note = 5;  // F
        //if (Input.GetKeyDown("5")) note = 7;  // G
        //if (Input.GetKeyDown("6")) note = 9;  // A
        //if (Input.GetKeyDown("7")) note = 11; // B
        //if (Input.GetKeyDown("8")) note = 12; // C
        //if (Input.GetKeyDown("9")) note = 14; // D

        if (note >= 0)
        { // if some key pressed...
            audio.pitch = Mathf.Pow(2, (note + transpose) / 12.0f);
            audio.Play();
        }
    }


    #region =========== Spells ==============

    // For Unit Testing
    public void CastLight() {
        // evoke delegate to tell all rooms to update light
        // if no rooms have subcribed to this event, than no rooms will be updated
        if (OnCastLightSpell != null) OnCastLightSpell(1);
    }

    public void CastFirebolt() { OnCastFireboltSpell(); }
    public void CastHeal() { OnCastHealSpell(5); }

    #endregion
}
