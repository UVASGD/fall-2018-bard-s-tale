using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    public bossAI Boss;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!static_information.isPaused)
        {

            if (Input.GetMouseButtonDown(0))
            {
                GameObject.Find("Boss").GetComponent<bossAI>().hit(damage: 1);
            }
            if (Input.GetMouseButtonDown(3))
            {
                bossAI clone = (bossAI)Instantiate(Boss, transform.position, transform.rotation);
            }
        }
    }
}
