﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkMoves();
	}
    
    void checkMoves()
    {
        Vector2 new_position = new Vector2(transform.position.x, transform.position.y);
        if (!static_information.isPaused)
        {
            if (Input.GetKey(static_information.controls[1]))
            {
                new_position.x -= 0.1f;
            }
            if (Input.GetKey(static_information.controls[3]))
            {
                new_position.x += 0.1f;
            }
            if (Input.GetKey(static_information.controls[2]))
            {
                new_position.y -= 0.1f;
            }
            if (Input.GetKey(static_information.controls[0]))
            {
                new_position.y += 0.1f;
            }
            transform.position = new_position;
        }
    }
}
