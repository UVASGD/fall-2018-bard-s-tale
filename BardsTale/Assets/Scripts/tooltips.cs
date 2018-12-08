using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltips : MonoBehaviour {

    private string[] tooltip_strings = new string[] {
        "Play the sequence J, K, L, ; and press SPACE to light a room!",
        "Play K J L J ; to cast fireball,\nL K ; J K ; to heal,\nand K J K J K J L K J to go fast!",
        "Kill the skeleton to exit the room!",
        "Kill the skeletons to exit the room!",
        "Kill the boss to exit the room!",
        "You've finished the game! Touch the purple tile to reach the credits!"
    };

    private Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = tooltip_strings[static_information.room_index];
	}
}
