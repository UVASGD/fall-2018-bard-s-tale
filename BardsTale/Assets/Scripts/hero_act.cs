using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_act : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start ()
    {
        health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Size of enemies list: " + static_information.enemies.Length);
            static_information.enemies[0].GetComponent<skeleton_act>().takeDamage();
        }
	}

    public void takeDamage()
    {
        health--;
        Debug.Log("Took damage! Health is " + health);
    }
}
