using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in SpellbookParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in ControlsParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
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
            bool keyPressed = false;
            // For UI
            if (!static_information.hotkeyWaiting)
            {
                // pause button checking
                if (Input.GetKeyDown(static_information.controls[12]))
                {
                    keyPressed = true;
                    // do some pause shit
                    Debug.Log("Pause button clicked!");
                    if (static_information.isPaused)
                    {
                        if (static_information.reading)
                        {
                            CloseBook();
                        }
                        else if (static_information.controlling)
                        {
                            ExitControls();
                        }
                        else
                        {
                            UnPause();
                        }
                    }
                    else
                    {
                        Pause();
                        static_information.isPaused = true;
                    }
                }
                // book button checking
                if (Input.GetKeyDown(static_information.controls[9]))
                {
                    keyPressed = true;
                    // do some book shit
                    Debug.Log("Book button clicked!");
                    if (!static_information.reading)
                    {
                        OpenBook();
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
                }

                // toggle whether or not we show keys
                if (!static_information.isPaused)
                {
                    if (Input.GetKeyDown(static_information.controls[15]))
                    {
                        static_information.isShowingKeys = !static_information.isShowingKeys;
                        if (static_information.isShowingKeys)
                        {
                            foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
                            {
                                i.enabled = true;
                            }
                        }
                        else
                        {
                            foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
                            {
                                i.enabled = false;
                            }
                        }
                    }
                }

                // if we are in the pause menu
                if (static_information.isPaused && !static_information.reading)
                {
                    if (Input.GetKeyDown(static_information.controls[14]))
                    {
                        ShowControls();
                    }
                }

                // if we are in the controls menu
                if (static_information.controlling)
                {
                    if (Input.GetKeyDown(static_information.controls[12]))
                    {
                        ExitControls();
                    }
                    else
                    {
                        for (int i = 0; i < static_information.controls.Length; i++)
                        {
                            if (Input.GetKeyDown(static_information.controls[i]))
                            {
                                changeIndex = i;
                                static_information.hotkeyWaiting = true;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (KeyCode k in static_information.set_of_all_possible_keys)
                {
                    if (Input.GetKey(k))
                    {
                        static_information.hotkeyWaiting = false;
                        static_information.controls[changeIndex] = k;
                        changeIndex = -1;
                    }
                }

                if (keyPressed) { cooldown = 3; }
            }
        }
    }

    private void Pause()
    {
        // set static_info to paused
        static_information.isPaused = true;

        // show pause menu UI
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }

        // un-show every set of menus, but not the AlwaysOns
        foreach (Image i in SpellbookParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
    }

    private void UnPause()
    {
        // set static_info to unpaused
        static_information.isPaused = false;

        // un-show pause menu UI
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }

        // if hideable stuff is unhidden, show hideable stuff
        if (static_information.isShowingKeys)
        {
            foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
            {
                i.enabled = true;
            }
        }
    }

    private void OpenBook()
    {
        // pause the game, open the book
        static_information.isPaused = true;
        static_information.reading = true;

        // show spellbook UI
        foreach (Image i in SpellbookParent.GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }

        // make sure every other UI set is un-shown
        foreach (Image i in HideableParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
        foreach (Image i in AlwaysOnParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
    }

    private void CloseBook()
    {
        // unpause the game, close the book
        static_information.isPaused = false;
        static_information.reading = false;

        // un-show spellbook UI
        foreach (Image i in SpellbookParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }

        // show Always-on stuff
        foreach (Image i in AlwaysOnParent.GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }
        
        // check to see if you re-add hideable UI
        if (static_information.isShowingKeys)
        {
            foreach(Image i in HideableParent.GetComponentsInChildren<Image>())
            {
                i.enabled = true;
            }
        }
    }

    private void ShowControls()
    {
        static_information.controlling = true;
        static_information.isPaused = true;
        foreach (Image i in ControlsParent.GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }

        // it is always in the pause menu before going here, so get rid of the pause menu
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }
    }

    private void ExitControls()
    {
        static_information.controlling = false;
        foreach (Image i in ControlsParent.GetComponentsInChildren<Image>())
        {
            i.enabled = false;
        }

        // we go back to the pause menu after this, so activate the pause menu
        foreach (Image i in PauseParent.GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }
    }
}
