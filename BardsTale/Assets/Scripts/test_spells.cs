using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_spells : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponentInChildren<self_spellcast_animation>().castSpell("fireball");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GetComponentInChildren<self_spellcast_animation>().castSpell("heal");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponentInChildren<self_spellcast_animation>().castSpell("light");
        }
	}
}
