using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_light : MonoBehaviour {

    public float alpha = 0;
    SpriteRenderer light_renderer;

	// Use this for initialization
	void Start () {
        light_renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        alpha = (alpha >= static_information.max_light_level - 15.0f)?static_information.max_light_level - 15.0f:
            (alpha + (0.001f * static_information.max_light_level));

        light_renderer.color = new Color(light_renderer.color.r, light_renderer.color.g, light_renderer.color.b, alpha/static_information.max_light_level);
	}

    public void addLight()
    {
        alpha -= static_information.max_light_level * 0.5f;
    }
}
