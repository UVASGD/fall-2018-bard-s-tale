using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end_the_game : MonoBehaviour {

    private float[] end_tile_xy = new float[] { 0.33f, 0.33f};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(static_information.hero.transform.position.x - transform.position.x) < end_tile_xy[0])
        {
            if (Mathf.Abs(static_information.hero.transform.position.y - transform.position.y) < end_tile_xy[1])
            {
                // go to the credits
                SceneManager.LoadScene(2);
            }
        }
	}
}
