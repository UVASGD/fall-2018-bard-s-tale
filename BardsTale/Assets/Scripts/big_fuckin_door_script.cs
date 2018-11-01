using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: THIS IS NOT A SCALABLE SOLUTION. I DON'T CARE; OUR DEMO COULD BE THIS WEEKEND AND IT'S CURRENTLY TUESDAY.
public class big_fuckin_door_script : MonoBehaviour {

    /* Let's quickly go over the doors in the demo:
     * Room 0: East door
     * Room 1: West door, east door (opened by fireball)
     * Room 2: West door (locked until dead skeleton), north door (opened by killing skeleton)
     * Room 3: South door (locked until dead skeletons), north door (opened by killing skeletons)
     * Room 4: South door (locked until dead boss), east door (opened by killing boss)
     * Room 5: West door
     * 
     * That's ten doors. 7 doors have conditional locks. We can check for these by giving each door a coordinate, and checking a radius for that coordinate.
     * Door 0: Room 0 east door
     * Door 1: Room 1 west door
     * Door 2: Room 1 east door
     * Door 3: Room 2 west door
     * Door 4: Room 2 north door
     * Door 5: Room 3 south door
     * Door 6: Room 3 north door
     * Door 7: Room 4 south door
     * Door 8: Room 4 east door
     * Door 9: Room 5 west door
     */
    public GameObject[] doors;

    // formerly 0.48f, 0.32f
    private Vector2 door_dimensions = new Vector2( 0.24f, 0.31f );

    private int door_cooldown;
    private int door_max_cooldown = 10;

	// Use this for initialization
	void Start ()
    {
        static_information.Awake();
        door_cooldown = 0;
        // Debug.Log("East Door location: " + doors[0].transform.position.x + ", " + doors[0].transform.position.y);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (door_cooldown > 0)
        {
            if (Input.GetKey(static_information.controls[0]) || Input.GetKey(static_information.controls[1]) || Input.GetKey(static_information.controls[2]) || Input.GetKey(static_information.controls[3]))
            { door_cooldown--; }
            return;
        }
        // Debug.Log("Door x Bound: " + (doors[0].transform.position.x - door_dimensions[0]) + ", hero x pos: " + static_information.hero.transform.position.x);
		for (int i = 0; i < doors.Length; i++)
        {
            if (is_in_doorway(static_information.hero.transform.position, doors[i].transform.position))
            {
                // Debug.Log("is_in_doorway returned true for door " + i + ".");
                switch (i)
                {
                    case 0:
                        // What to do if the hero has entered door 0
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(1, 1);
                        break;
                    case 1:
                        // What to do if the hero has entered door 1
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(0, 3);
                        break;
                    case 2:
                        // What to do if the hero has entered door 2
                        // Condition: need to have casted fireball
                        if (static_information.has_casted_fireball)
                        {
                            static_information.camera.GetComponent<room_change_listening>().MoveToRoom(2, 1);
                        }
                        break;
                    case 3:
                        // What to do if the hero has entered door 3
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(1, 3);
                        break;
                    case 4:
                        // What to do if the hero has entered door 4
                        // Condition: all enemies in room must be dead
                        if (static_information.is_the_current_room_clear())
                        {
                            static_information.camera.GetComponent<room_change_listening>().MoveToRoom(3, 0);
                        }
                        break;
                    case 5:
                        // What to do if the hero has entered door 5
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(2, 2);
                        break;
                    case 6:
                        // What to do if the hero has entered door 6
                        // Condition: all enemies in room must be dead
                        if (static_information.is_the_current_room_clear())
                        {
                            static_information.camera.GetComponent<room_change_listening>().MoveToRoom(4, 0);
                        }
                        break;
                    case 7:
                        // What to do if the hero has entered door 7
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(3, 2);
                        break;
                    case 8:
                        // What to do if the hero has entered door 8
                        // Condition: all enemies in room must be dead
                        if (static_information.is_the_current_room_clear())
                        {
                            static_information.camera.GetComponent<room_change_listening>().MoveToRoom(5, 1);
                        }
                        break;
                    case 9:
                        // What to do if the hero has entered door 9
                        static_information.camera.GetComponent<room_change_listening>().MoveToRoom(4, 3);
                        break;
                    default:
                        Debug.Log("What the actual fuck.");
                        break;
                }
                break;
            }
        }
        door_cooldown = door_max_cooldown;
	}

    bool is_in_doorway(Vector2 person_position, Vector2 door_position)
    {
        //Debug.Log("person_position x: " + person_position.x + ", door_position x: " + door_position.x
        //    + "\ndifference x: " + Mathf.Abs(person_position.x - door_position.x) + ", dim x: " + (door_dimensions.x));
        //Debug.Log("person_position y: " + person_position.y + ", door_position y: " + door_position.y
        //    + "\ndifference y: " + Mathf.Abs(person_position.y - door_position.y) + ", dim y: " + (door_dimensions.y));
        if (Mathf.Abs(person_position.x - door_position.x) <= door_dimensions.x)
        {
            if (Mathf.Abs(person_position.y - door_position.y) <= door_dimensions.y)
            {
                return true;
            }
        }
        return false;
    }
}
