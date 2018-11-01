using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_change_listening : MonoBehaviour {

    // Secret Keys to change rooms
    private KeyCode[] room_indices = new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };

    protected float[,] room_coordinates = new float[,] { { 0.0f, 0.0f }, { 4.8f, 0.0f }, { 9.6f, 0.0f }, { 9.6f, 3.6f }, { 9.6f, 7.2f }, { 14.4f, 7.2f } };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    for (int i = 0; i < room_indices.Length; i++)
        {
            if (Input.GetKeyDown(room_indices[i]))
            {
                transform.position = new Vector3 (room_coordinates[i, 0], room_coordinates[i, 1], -10.0f);
                static_information.hero.transform.position = new Vector2(room_coordinates[i, 0], room_coordinates[i, 1]);
                static_information.room_index = static_information.which_room_am_I_in(static_information.hero.transform.position.x, static_information.hero.transform.position.y);
                if (static_information.room_index == -1)
                {
                    Debug.Log("Hate to see it. X = " + static_information.hero.transform.position.x + ", Y= " + static_information.hero.transform.position.y);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveToRoom(1, 1);
        }
	}

    public void MoveToRoom (int room_index, int direction)
    {
        // If moving 0 (or 2), add (or subtract) 1.24.
        // If moving 1 (or 3), add (or subtract) 1.02.
        switch (direction)
        {
            case 0:
                static_information.hero.transform.position = new Vector2(static_information.hero.transform.position.x, static_information.hero.transform.position.y + 1.24f);
                break;
            case 1:
                static_information.hero.transform.position = new Vector2(static_information.hero.transform.position.x + 1.02f, static_information.hero.transform.position.y);
                break;
            case 2:
                static_information.hero.transform.position = new Vector2(static_information.hero.transform.position.x, static_information.hero.transform.position.y - 1.24f);
                break;
            case 3:
                static_information.hero.transform.position = new Vector2(static_information.hero.transform.position.x - 1.02f, static_information.hero.transform.position.y);
                break;
            default:
                Debug.Log("Something's wrong.");
                break;
        }
        transform.position = new Vector3(room_coordinates[room_index, 0], room_coordinates[room_index, 1], -10.0f);

        static_information.room_index = static_information.which_room_am_I_in(static_information.hero.transform.position.x, static_information.hero.transform.position.y);
    }
}
