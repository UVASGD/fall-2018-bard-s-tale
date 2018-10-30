using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // TODO: should handle room transitions and have a concept of the board
    // Need to replace the sprites cause idk how to use the ase's
    // Physics supposedly should work, but need to set the tags and layers
    // Would like to get just an outer wall tile instead of the entire wall since the doors should be variable 
    // Speaking of doors, they should be variable based on what rooms are available around them - handle-able
    // By this class and the supposed future array of rooms and some math handling and booleans inside Room
    public static GameManager instance = null;
    private Room room;

	// Use this for initialization
	void Awake () {
        if (instance == null)  // This should work on transitions? not fleshed out, but idea is to destroy current room and then spawn the new one
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        room = GetComponent<Room>();

        InitGame();
	}
	
    void InitGame()
    {
        room.SetupScene();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
