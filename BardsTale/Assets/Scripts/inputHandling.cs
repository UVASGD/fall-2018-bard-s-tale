using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inputHandling : MonoBehaviour {

    //how many frames does it take before inputting new input?
    public int cooldown = 0;

    //which concrete UI values can set the visibility and interactibility of separate UIs?
    public Text PauseParent = null;
    public Text SpellbookParent = null;
    public Text HideableParent = null;
    public Text AlwaysOnParent = null;
    public Text ControlsParent = null;

    private int changeIndex = -1;

	// Use this for initialization
	void Start () {
        //PauseParent = GameObject.Find("canvas/Pause Menu Parent").GetComponent<Text>();
        //SpellbookParent = GameObject.Find("canvas/Spellbook Parent").GetComponent<Text>();
        setVisibility(PauseParent, false);
        setVisibility(SpellbookParent, false);
        setVisibility(HideableParent, false);
        setVisibility(ControlsParent, false);
	}

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            // For UI
            if (!static_information.hotkeyWaiting)
            {
                // if we are in the overworld
                if (!static_information.controlling && !static_information.reading && !static_information.isPaused)
                {
                    // pause button checking
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(static_information.controls[12]))
                    {
                        // do some pause shit
                        Debug.Log("Pause button clicked!");
                        Pause();
                        return;
                    }

                    ///LIGHT SPELL TESTING
                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }

                    // book button checking
                    if (Input.GetKeyDown(static_information.controls[9]))
                    {
                        // do some book shit
                        Debug.Log("Book button clicked!");
                        OpenBook();
                        return;
                    }

                    // toggle showing the keys (not available during controls menu, spellbook, or pause menu)
                    if (Input.GetKeyDown(static_information.controls[13]))
                    {
                        Debug.Log("Toggling keys!");
                        static_information.isShowingKeys = !static_information.isShowingKeys;
                        if (static_information.isShowingKeys)
                        {
                            setVisibility(HideableParent, true);
                        }
                        else
                        {
                            setVisibility(HideableParent, false);
                        }
                    }

                    // movement and music are handled by the PC prefab and music interpretation engine, respectively, but we will mess with the UI for these too
                    if (static_information.isShowingKeys)
                    {
                        foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
                        {
                            // check each UI element, 'cause we don't know which key is being pressed, exactly
                            // movement keys
                            if (i.name.Equals("Up"))
                            {
                                i.color = (Input.GetKey(static_information.controls[0])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Left"))
                            {
                                i.color = (Input.GetKey(static_information.controls[1])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Down"))
                            {
                                i.color = (Input.GetKey(static_information.controls[2])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Right"))
                            {
                                i.color = (Input.GetKey(static_information.controls[3])) ? Color.green : Color.red;
                            }

                            // note keys
                            if (i.name.Equals("Note 1"))
                            {
                                i.color = (Input.GetKey(static_information.controls[4])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Note 2"))
                            {
                                i.color = (Input.GetKey(static_information.controls[5])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Note 3"))
                            {
                                i.color = (Input.GetKey(static_information.controls[6])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Note 4"))
                            {
                                i.color = (Input.GetKey(static_information.controls[7])) ? Color.green : Color.red;
                            }
                            if (i.name.Equals("Send Notes"))
                            {
                                i.color = (Input.GetKey(static_information.controls[8])) ? Color.green : Color.red;
                            }

                        }
                        return;
                    }
                }
                
                // if we are in the pause menu
                if (static_information.isPaused && !static_information.reading && !static_information.controlling)
                {
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(static_information.controls[12]))
                    {
                        // unpause the game
                        UnPause();
                    }
                    if (Input.GetKeyDown(static_information.controls[9]))
                    {
                        // open spellbook
                        OpenBook();
                    }
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        // open controls menu
                        ShowControls();
                        // don't want to replace C with C
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.N))
                    {
                        // unpause the game, set everything back to defaults
                        static_information.reading = false;
                        static_information.controlling = false;
                        static_information.isShowingKeys = false;
                        static_information.isPaused = false;

                        // change scenes to start menu
                        SceneManager.LoadScene(static_information.start_menu_scene_index);
                    }
                }

                // if we are in the spellbook menu
                if (static_information.reading && static_information.isPaused)
                {
                    if (Input.GetKeyDown(static_information.controls[12]) || Input.GetKeyDown(KeyCode.Escape))
                    {
                        CloseBook();
                    }
                    else
                    {
                        // go through the spellbook shit
                        foreach (Image i in SpellbookParent.GetComponentsInChildren<Image>())
                        {
                            // there's an image called space which references the keybinding that sends notes
                            // it's by default the second image, so we can find it that way
                            Image cache = SpellbookParent.GetComponentsInChildren<Image>()[1];
                            if (i.name.Equals("Space"))
                            {
                                i.color = (Input.GetKey(static_information.controls[8])) ? Color.green : Color.red;
                                if (Input.GetKeyDown(static_information.controls[8]))
                                {
                                    Text t = cache.GetComponentsInChildren<Text>()[0];
                                    Debug.Log("Text cleared!");
                                    t.text = "";
                                }
                            }
                            if (i.name.Equals("Note 1"))
                            {
                                i.color = (Input.GetKey(static_information.controls[4])) ? Color.green : Color.red;
                                if (Input.GetKeyDown(static_information.controls[4]))
                                {
                                    if (cache.GetComponentsInChildren<Text>()[0].text.Equals("Notes you enter will appear here!"))
                                    {
                                        cache.GetComponentsInChildren<Text>()[0].text = "";
                                    }
                                    cache.GetComponentsInChildren<Text>()[0].text += charify(static_information.controls[4].ToString());
                                }
                            }
                            if (i.name.Equals("Note 2"))
                            {
                                i.color = (Input.GetKey(static_information.controls[5])) ? Color.green : Color.red;
                                if (Input.GetKeyDown(static_information.controls[5]))
                                {
                                    if (cache.GetComponentsInChildren<Text>()[0].text.Equals("Notes you enter will appear here!"))
                                    {
                                        cache.GetComponentsInChildren<Text>()[0].text = "";
                                    }
                                    cache.GetComponentsInChildren<Text>()[0].text += charify(static_information.controls[5].ToString());
                                }
                            }
                            if (i.name.Equals("Note 3"))
                            {
                                i.color = (Input.GetKey(static_information.controls[6])) ? Color.green : Color.red;
                                if (Input.GetKeyDown(static_information.controls[6]))
                                {
                                    if (cache.GetComponentsInChildren<Text>()[0].text.Equals("Notes you enter will appear here!"))
                                    {
                                        cache.GetComponentsInChildren<Text>()[0].text = "";
                                    }
                                    cache.GetComponentsInChildren<Text>()[0].text += charify(static_information.controls[6].ToString());
                                }
                            }
                            if (i.name.Equals("Note 4"))
                            {
                                i.color = (Input.GetKey(static_information.controls[7])) ? Color.green : Color.red;
                                if (Input.GetKeyDown(static_information.controls[7]))
                                {
                                    if (cache.GetComponentsInChildren<Text>()[0].text.Equals("Notes you enter will appear here!"))
                                    {
                                        cache.GetComponentsInChildren<Text>()[0].text = "";
                                    }
                                    cache.GetComponentsInChildren<Text>()[0].text += charify(static_information.controls[7].ToString());
                                }
                            }

                        }
                    }
                }

                // if we are in the controls menu
                if (static_information.controlling)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ExitControls();
                    }
                    else
                    {
                        // THE IMAGES WITHIN THE "KEYS" IMAGE ARE IN A SACRED (same as static_information.controls) ORDER
                        Image[] keyImages = ControlsParent.GetComponentsInChildren<Image>();

                        for (int i = 0; i < static_information.controls.Length; i++)
                        {
                            if (Input.GetKeyDown(static_information.controls[i]))
                            {
                                changeIndex = i;

                                // for testing, figure out which button got pressed
                                Debug.Log(changeIndex + ", " + charify(static_information.controls[changeIndex].ToString()));

                                keyImages[i].color = Color.red;

                                static_information.hotkeyWaiting = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    Debug.Log("Hotkeywaiting value: " + static_information.hotkeyWaiting);

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        // change hotkeyWaiting
                        static_information.hotkeyWaiting = false;

                        // reset images to white
                        foreach (Image i in ControlsParent.GetComponentsInChildren<Image>())
                        {
                            i.color = Color.white;
                        }

                        Debug.Log("Cancelled.");
                    }
                    else
                    {
                        string debug_old_key = charify(static_information.controls[changeIndex].ToString());

                        // assign new hotkey
                        foreach (KeyCode k in static_information.set_of_all_possible_keys)
                        {
                            // got the key pressed
                            if (Input.GetKeyDown(k))
                            {
                                static_information.hotkeyWaiting = false;
                                static_information.controls[changeIndex] = k;
                            }
                        }

                        // change the display to accommodate
                        foreach (Image i in ControlsParent.GetComponentsInChildren<Image>())
                        {
                            // the selected keybinding will be red
                            if (i.color.Equals(Color.red))
                            {
                                i.color = Color.white;
                                i.GetComponentInChildren<Text>().text = charify(static_information.controls[changeIndex].ToString());
                            }
                        }

                        Debug.Log("Replaced " + debug_old_key + " with " + charify(static_information.controls[changeIndex].ToString()) + ".");
                    }

                    changeIndex = -1;
                }
            }
        }
    }

    private void Pause()
    {
        // set static_info to paused
        static_information.isPaused = true;

        // show pause menu UI
        setVisibility(PauseParent, true);

        // un-show every set of menus, but not the AlwaysOns
        setVisibility(SpellbookParent, false);
        setVisibility(HideableParent, false);
        setVisibility(ControlsParent, false);
    }

    private void UnPause()
    {
        // set static_info to unpaused
        static_information.isPaused = false;

        // un-show pause menu UI
        setVisibility(PauseParent, false);
        
        // if hideable stuff is unhidden, show hideable stuff
        if (static_information.isShowingKeys)
        {
            setVisibility(HideableParent, true);
        }
    }

    private void OpenBook()
    {
        // pause the game, open the book
        static_information.isPaused = true;
        static_information.reading = true;

        // show spellbook UI
        setVisibility(SpellbookParent, true);

        // make sure every other UI set is un-shown
        setVisibility(HideableParent, false);
        setVisibility(PauseParent, false);
        setVisibility(AlwaysOnParent, false);
        setVisibility(ControlsParent, false);
    }

    private void CloseBook()
    {
        // unpause the game, close the book
        static_information.isPaused = false;
        static_information.reading = false;

        // un-show spellbook UI
        setVisibility(SpellbookParent, false);
        
        // show Always-on stuff
        setVisibility(AlwaysOnParent, true);
                
        // check to see if you re-add hideable UI
        if (static_information.isShowingKeys)
        {
            setVisibility(HideableParent, true);
        }
    }

    private void ShowControls()
    {
        static_information.controlling = true;
        static_information.isPaused = true;
        setVisibility(ControlsParent, true);

        // it is always in the pause menu before going here, so get rid of the pause menu
        setVisibility(PauseParent, false);

        // for quality control, make sure everything else is un-shown
        setVisibility(AlwaysOnParent, false);
        setVisibility(SpellbookParent, false);
        setVisibility(HideableParent, false);
    }

    private void ExitControls()
    {
        static_information.controlling = false;
        setVisibility(ControlsParent, false);

        // we go back to the pause menu after this, so activate the pause menu
        setVisibility(PauseParent, true);
    }

    // Assumes the @param parent is a UI Text, with Image children, who have Text children
    // Sets the enabled status of each Image and its text child to @param visibility
    private void setVisibility(Text parent, bool visibility)
    {
        foreach (Image i in parent.GetComponentsInChildren<Image>())
        {
            foreach (Text t in i.GetComponentsInChildren<Text>())
            {
                t.enabled = visibility;
            }
            i.enabled = visibility;
        }
    }

    // Assumes the @param input is the toString of a KeyCode, and turns it into a 
    // shorter, more pallatable string to be displayed on an image's text box.
    public static string charify(string input)
    {
        string toReturn = input;

        // Turn numbers from "Alpha#" to just "#"
        if (toReturn.Length > 5 && toReturn.Substring(0, 5).Equals("Alpha"))
        {
            toReturn = toReturn.Substring(5);
        }

        // Turn arrow keys from "<Dir>Arrow" to just "<Dir>"
        if (toReturn.Length > 5 && toReturn.Substring(toReturn.Length - 5).Equals("Arrow"))
        {
            toReturn = toReturn.Substring(0, toReturn.Length - 5);
        }

        // Turn keypads from "Keypad#" to just "k#"
        if (toReturn.Length > 6 && toReturn.Substring(0, 6).Equals("Keypad"))
        {
            toReturn = "k" + toReturn.Substring(6);
        }

        switch (toReturn)
        {
            case "Semicolon":
                toReturn = ";";
                break;
            case "LeftShift":
                toReturn = "lsh";
                break;
            case "Space":
                toReturn = "spc";
                break;
        }

        return toReturn;
    }
}
