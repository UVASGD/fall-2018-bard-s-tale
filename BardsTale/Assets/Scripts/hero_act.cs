using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hero_act : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start ()
    {
        health = 5;
        static_information.Awake();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (static_information.isPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                static_information.has_casted_fireball = true;
                foreach (GameObject g in static_information.enemies)
                {
                    if (static_information.is_in_bounds(g.transform.position))
                    {
                        if (g.GetComponent<skeleton_act>().is_dead == false)
                        {
                            // Debug.Log("Size of enemies list: " + static_information.enemies.Length);
                            g.GetComponent<skeleton_act>().takeDamage();
                            break;
                        }
                    }
                }
            }
        }*/
	}

    public void takeDamage()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("Hero Died!");
            SceneManager.LoadScene(0);
        }
        GameObject.Find("Health Parent").GetComponent<adjustHealth>().setHealth(health);
        //Debug.Log("Took damage! Health is " + health);
    }

    public void healDamage()
    {
        if (health < 5)
        {
            health++;
        }
        GameObject.Find("Health Parent").GetComponent<adjustHealth>().setHealth(health);
    }
}
