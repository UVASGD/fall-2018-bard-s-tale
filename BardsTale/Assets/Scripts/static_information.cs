using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class static_information {
    // USE THIS TO REFERENCE PLAYER LOCATION
    public static float[] player_position = new float[] { };

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

    // USE THIS TO SEE WHAT ROOM THE BARD IS IN
    public static int bard_room_location = 0;

    // USE THIS TO GET LIGHT-LEVELS IN ROOMS
    public static float[] room_light_levels = new float[]
        { 0,0,0,0,0,0};

    //USE THIS TO GET THE MAXIMUM LIGHT VALUE
    //A.K.A.: how many seconds until the light hits the minimum light value
    public static float max_light_level = 30;
}
