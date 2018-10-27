using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_move : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
        checkMoves();
	}

    void checkMoves()
    {
        Vector2 new_position = new Vector2(transform.position.x, transform.position.y);

        // If moving in two directions (diagonally), this equals 2. If moving in one direction (orthogonally), this equals 1.
        // If not moving, this equals 0, which might indicate that we could divide by 0, but we can't.
        int count_effective_moves = 
            ((Input.GetKey(static_information.controls[0]) || Input.GetKey(static_information.controls[2])) ? 1 : 0) + 
            ((Input.GetKey(static_information.controls[1]) || Input.GetKey(static_information.controls[3])) ? 1 : 0);

        // sqrt(2) means speed is same everywhere, since a 45 degree (diagonal) line is speed times sqrt(2)
        if (Input.GetKey(static_information.controls[0])) // up
        {
            new_position.y += speed / Mathf.Sqrt(count_effective_moves);
        }
        if (Input.GetKey(static_information.controls[2])) // down
        {
            new_position.y -= speed / Mathf.Sqrt(count_effective_moves);
        }
        if (Input.GetKey(static_information.controls[1])) // left
        {
            new_position.x -= speed / Mathf.Sqrt(count_effective_moves);
        }
        if (Input.GetKey(static_information.controls[3])) // right
        {
            new_position.x += speed / Mathf.Sqrt(count_effective_moves);
        }
        transform.position = new_position;
        static_information.hero.transform.position = transform.position;
    }
}
