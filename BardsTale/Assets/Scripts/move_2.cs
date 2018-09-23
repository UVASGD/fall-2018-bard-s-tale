using System.Collections;
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

        if (Input.GetKey(KeyCode.A))
        {
            new_position.x -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            new_position.x += 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            new_position.y -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            new_position.y += 0.1f;
        }
        transform.position = new_position;
    }
}
