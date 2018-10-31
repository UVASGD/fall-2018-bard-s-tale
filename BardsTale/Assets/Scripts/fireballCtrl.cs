using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCtrl : MonoBehaviour {
    Animator anim;
    public Vector2 speed;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("collide");
        Destroy(gameObject, .4f);
    }
}
