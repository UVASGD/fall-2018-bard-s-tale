using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class static_information {
    // USE THIS TO REFERENCE HOTKEYS, can easily verify within scripts
    /* Index    Function        Default Hotkey (as a string)
     * 0        up              w    
     * 1        left            a
     * 2        down            s
     * 3        right           d
     * 4        note1           j
     * 5        note2           k
     * 6        note3           l
     * 7        note4           ;
     * 8        sendnotes       space
     * 9        spellbk         b
     * 10       useitem         lshift
     * 11       pickup          e
     * 12       pause           p
     * 13       toggle help     v
     */
    public static KeyCode[] controls = new KeyCode[] 
        {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.J, KeyCode.K, KeyCode.L,
            KeyCode.Semicolon, KeyCode.Space, KeyCode.B, KeyCode.LeftShift, KeyCode.E,
            KeyCode.P, KeyCode.V};

    // USE THIS AS A WHITELIST OF USABLE KEYS FOR KEYBINDINGS
    public static KeyCode[] set_of_all_possible_keys = new KeyCode[]
        {KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G,
            KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N,
            KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U,
            KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z, KeyCode.Alpha0, KeyCode.Alpha1,
            KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
            KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Minus, KeyCode.Equals, KeyCode.LeftBracket, KeyCode.RightBracket,
            KeyCode.Backslash, KeyCode.BackQuote, KeyCode.Backspace, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Comma,
            KeyCode.Period, KeyCode.Slash, KeyCode.Space, KeyCode.LeftShift, KeyCode.RightShift, KeyCode.UpArrow,
            KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2,
            KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8,
            KeyCode.Keypad9, KeyCode.KeypadDivide, KeyCode.KeypadEnter, KeyCode.KeypadEquals, KeyCode.KeypadMinus,
            KeyCode.KeypadMultiply, KeyCode.KeypadPeriod, KeyCode.KeypadPlus, KeyCode.Tab, KeyCode.CapsLock, KeyCode.Escape};

    // USE THIS TO SEE IF GAME IS PAUSED
    public static bool isPaused = false;
    // OBJECTS WHICH DO ANYTHING DURING THE GAMEPLAY NEED TO PAY ATTENTION TO THIS

    // USE THIS TO SEE IF THE SPELLBOOK IS OPEN
    public static bool reading = false;
    // AUDIO OUTPUT HANDLING NEEDS TO PAY ATTENTION TO THIS AND ISPAUSED

    // USE THIS TO SEE IF GAME IS ON THE CONTROLS SCREEN
    public static bool controlling = false;
    // ALL KEYBOARD INPUT NEED TO PAY ATTENTION TO THIS

    // USE THIS TO SEE IF GAME IS WAITING ON A HOTKEY
    public static bool hotkeyWaiting = false;
    // ALL INPUT NEEDS TO PAY ATTENTION TO THIS

    // USE THIS TO SEE IF GAME IS DISPLAYING KEYS
    public static bool isShowingKeys = false;
    // NOTHING OUTSIDE OF UI NEEDS TO PAY ATTENTION TO THIS

    // USE THESE TO HELP WITH SCENE MANAGEMENT
    public static int start_menu_scene_index = 0;
    public static int new_game_scene_index = 1;

    // USE THIS TO HELP TRACK THE PC
    public static GameObject hero;
    public static GameObject camera;
    public static GameObject[] enemies;
    public static GameObject[] fireballs;

    public static void Awake()
    {
        hero = GameObject.Find("Hero");
        camera = GameObject.Find("Main Camera");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        fireballs = GameObject.FindGameObjectsWithTag("Fireball");
    }

    // USE THIS TO HELP CHANGE CAMERA FOR ROOMS (x_offset, y_offset)
    public static float[] camera_corner_offset = new float[] { 2.4f - 0.51f, 1.8f - 0.4f};

    // USE THIS TO TELL US WHICH ROOM YOU'RE IN
    public static int room_index = 0;

    // USE THIS TO CHANGE LIGHT LEVELS
    public static float max_light_level = 30.0f;
    public static float[] room_light_levels = new float[] { 0, 0, 0, 0, 0, 0 };//im pretty sure no one is actually using this, and it's useless so uhhh don't use it
    public static float maximumDarknessOpacity = 0.06f;

    // USE THESE FOR SOME DOOR CONDITIONS
    public static bool has_casted_fireball = false; 

    public static bool is_the_current_room_clear()
    {
        foreach (GameObject g in enemies)
        {
            if (is_in_bounds(g.transform.position))
            {
                // Debug.Log("Found an enemy. Its life status: " + (g.GetComponent<skeleton_act>().is_dead ? "dead." : "alive."));
                if (g.GetComponent<skeleton_act>().is_dead == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // USE THIS TO FIGURE OUT WHICH ROOM YOU'RE IN
    public static int which_room_am_I_in(float x_coord, float y_coord)
    {
        // nah, man. this is totally efficient, shut up
        if (x_coord < 2.4 && x_coord > -2.4)
        {
            return 0;
        }
        else if (x_coord < 16.8 && x_coord > 12.0)
        {
            return 5;
        }
        else if (x_coord > 2.4 && x_coord < 7.2)
        {
            return 1;
        }
        else
        {
            if (y_coord <  1.8 && y_coord > -1.8)
            {
                return 2;
            }
            else if (y_coord < 5.4 && y_coord > 1.8)
            {
                return 3;
            }
            else if (y_coord < 8.2 && y_coord > 5.4)
            {
                return 4;
            }
        }
        return -1;
    }

    public static bool is_in_bounds(Vector2 position)
    {
        // grab important characteristics from main camera
        Vector2 central_cam_position = camera.transform.position;
        Vector2 bottom_left_corner = new Vector2(central_cam_position.x - camera_corner_offset[0], central_cam_position.y - camera_corner_offset[1]);
        Vector2 top_right_corner = new Vector2(central_cam_position.x + camera_corner_offset[0], central_cam_position.y + camera_corner_offset[1]);
        if (bottom_left_corner.x < position.x && position.x < top_right_corner.x)
        {
            if (bottom_left_corner.y < position.y && position.y < top_right_corner.y)
            {
                return true;
            }
        }
        return false;
    }

    public static bool is_in_x_bounds(Vector2 position)
    {
        // grab important characteristics from main camera
        Vector2 central_cam_position = camera.transform.position;
        Vector2 bottom_left_corner = new Vector2(central_cam_position.x - camera_corner_offset[0], central_cam_position.y - camera_corner_offset[1]);
        Vector2 top_right_corner = new Vector2(central_cam_position.x + camera_corner_offset[0], central_cam_position.y + camera_corner_offset[1]);
        if (bottom_left_corner.x < position.x && position.x < top_right_corner.x)
        {
            return true;
        }
        return false;
    }

    public static bool is_in_y_bounds(Vector2 position)
    {
        // grab important characteristics from main camera
        Vector2 central_cam_position = camera.transform.position;
        Vector2 bottom_left_corner = new Vector2(central_cam_position.x - camera_corner_offset[0], central_cam_position.y - camera_corner_offset[1]);
        Vector2 top_right_corner = new Vector2(central_cam_position.x + camera_corner_offset[0], central_cam_position.y + camera_corner_offset[1]);
        if (bottom_left_corner.y < position.y && position.y < top_right_corner.y)
        {
            return true;
        }
        return false;
    }
}
