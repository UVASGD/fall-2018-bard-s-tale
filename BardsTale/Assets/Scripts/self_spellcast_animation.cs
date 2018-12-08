using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_spellcast_animation : MonoBehaviour {

    public string fireball_path;
    public string heal_path;
    public string light_path;
    //public string speed_path;

    private Sprite[] fireball_animation;
    private Sprite[] heal_animation;
    private Sprite[] light_animation;
    //private Sprite[] speed_animation;

    private Sprite[] current_animation;

    private int animation_index;
    private int animation_length;

    private int cooldown;
    private int max_cooldown = 4;

    private SpriteRenderer r;

	// Use this for initialization
	void Start () {
        fireball_animation = Resources.LoadAll<Sprite>(fireball_path);
        heal_animation = Resources.LoadAll<Sprite>(heal_path);
        light_animation = Resources.LoadAll<Sprite>(light_path);
        //speed_animation = Resources.LoadAll<Sprite>(speed_path);

        r = GetComponent<SpriteRenderer>();

        animation_index = 0;
        animation_length = 1;
        cooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (cooldown > 0)
        {
            cooldown--;
            return;
        }

		if (current_animation == null)
        {
            r.sprite = null;
            animation_index = 0;
            animation_length = 1;
        }
        else
        {
            r.sprite = current_animation[++animation_index % animation_length];
            if (animation_index % animation_length == 0)
            {
                current_animation = null;
                animation_index = 0;
                animation_length = 0;
            }
            cooldown = max_cooldown;
        }
	}

    public void castSpell(string spellName)
    {
        if (spellName.Equals("fireball"))
        {
            current_animation = fireball_animation;
        }
        if (spellName.Equals("heal"))
        {
            current_animation = heal_animation;
        }
        if (spellName.Equals("light"))
        {
            current_animation = light_animation;
        }
        animation_length = current_animation.Length;
        
    }
}
