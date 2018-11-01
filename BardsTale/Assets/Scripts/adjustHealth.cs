using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WITHIN HEALTH_PARENT, ARE FIVE HEARTS (Heart 1, Heart 2, Heart 3, Heart 4, Heart 5)
public class adjustHealth : MonoBehaviour {

    Image[] hearts;

	// Use this for initialization
	void Start () {
        hearts = GetComponentsInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHealth(int health)
    {
        if (health < 0 || health > 5)
        {
            return;
        }
        for (int i = 0; i < 5; i++)
        {
            if (i < health)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
