using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class credits : MonoBehaviour {

    public Text directors;
    public Text programmers;
    public Text artists;

    private int state;

	// Use this for initialization
	void Start () {
        directors.enabled = true;
        programmers.enabled = false;
        artists.enabled = false;
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if ((state == 0 || state == 2) && Input.GetKeyDown(KeyCode.A))
        {
            directors.enabled = false;
            programmers.enabled = false;
            artists.enabled = true;
            state = 1;
        }
        if ((state == 1 || state == 0) && Input.GetKeyDown(KeyCode.P))
        {
            directors.enabled = false;
            artists.enabled = false;
            programmers.enabled = true;
            state = 2;
        }
        if ((state == 2 || state == 1) && Input.GetKeyDown(KeyCode.D))
        {
            directors.enabled = true;
            artists.enabled = false;
            programmers.enabled = false;
            state = 0;
        }
	}
}
