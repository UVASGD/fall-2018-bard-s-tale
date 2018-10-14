using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JKL; (light)
// KJLJ; (firebolt)
// LK;JK; (health)
// LLLKL;J (super mario)
// ;;;KLLLJ (Bethoveen's 5th)

// space to cast

public class SpellCasting : MonoBehaviour {

    protected string currentString = "";
    protected int startKey = -1;
    protected int[] lightSpellSequence = new int[] { 4, 5, 6, 7 };
    protected int[] boltSpellSequence = new int[] { 5, 4, 6, 4, 7 };
    protected int[] healSpellSequence = new int[] { 6, 5, 7, 4, 5, 7 };
    protected int[] marioSpellSequence = new int[] { 6, 6, 6, 5, 6, 7, 4 };
    protected int[] beethoven5thSpellSequence = new int[] { 7, 7, 7, 5, 6, 6, 6, 4 };
    protected int[][] sequencesList;
    protected string[] baseSpellList;

    public AudioSource audio;

	// Use this for initialization
	void Start () {
        sequencesList = new int[][] { lightSpellSequence, boltSpellSequence, healSpellSequence, marioSpellSequence, beethoven5thSpellSequence };
        baseSpellList = new string[sequencesList.Length];
        for(int i = 0; i < sequencesList.Length; i++)
        {
            string spellString = "";
            for(int j = 0; j < sequencesList[i].Length; j++)
            {
                spellString += inputHandling.charify(static_information.controls[sequencesList[i][j]].ToString());
            }
            baseSpellList[i] = spellString;
            Debug.Log(baseSpellList[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(static_information.controls[4]))
        {
            currentString += inputHandling.charify(static_information.controls[4].ToString());
            if(startKey == -1)
            {
                startKey = 4;
            }
            playTone(4, startKey);

            Debug.Log(currentString);
        }
        else if (Input.GetKeyDown(static_information.controls[5]))
        {
            currentString += inputHandling.charify(static_information.controls[5].ToString());
            if (startKey == -1)
            {
                startKey = 5;
            }
            playTone(5, startKey);

            Debug.Log(currentString);
        }
        else if (Input.GetKeyDown(static_information.controls[6]))
        {
            currentString += inputHandling.charify(static_information.controls[6].ToString());
            if (startKey == -1)
            {
                startKey = 6;
            }
            playTone(6, startKey);

            Debug.Log(currentString);
        }
        else if (Input.GetKeyDown(static_information.controls[7]))
        {
            currentString += inputHandling.charify(static_information.controls[7].ToString());
            if (startKey == -1)
            {
                startKey = 7;
            }
            playTone(7, startKey);

            Debug.Log(currentString);
        }
        else if (Input.GetKeyDown(static_information.controls[8]))
        {
            for(int i = 0; i < baseSpellList.Length; i++)
            {
                if(currentString.Equals(baseSpellList[i]))
                {
                    Debug.Log("Successful Spell");
                    currentString = "";
                    startKey = -1;
                    return;
                }
            }
            Debug.Log("Spell Failed");
            currentString = "";
            startKey = -1;
        }
    }

    //plays the note
    protected void playTone(int key, int start)
    {
        Debug.Log("Played: " + key + ", " + start);

        var note = -1;
        var transpose = -4;

        switch (key)
        {
            case 4:
                switch (start)
                {
                    case 4:
                        break;
                    case 5:
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
                        break;
                    case 5:
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
                        break;
                    case 5:
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
                        break;
                    case 5:
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
}
