using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_menu_animation : MonoBehaviour {

    public string path;
    Sprite[] spritesList;

    int animationLength = 8;
    int counter = 0;
    SpriteRenderer r;
    int cooldown_max = 7;
    int cooldown = 0;

	// Use this for initialization
	void Start ()
    {
        spritesList = Resources.LoadAll<Sprite>(path);
        r = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cooldown > 0)
        {
            cooldown--;
            return;
        }

        cooldown = cooldown_max;
        r.sprite = spritesList[counter++ % animationLength];
	}
}
