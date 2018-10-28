using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu_new_game_handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.N))
        {
            // change scenes
            SceneManager.LoadScene(static_information.new_game_scene_index);
        }
	}
}
