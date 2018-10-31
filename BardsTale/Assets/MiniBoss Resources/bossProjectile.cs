using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProjectile : MonoBehaviour {

    //stats
    public int maxHealth = 1;
    public int health = 1;
    public int damage = 1;
    private float baseSpeed = 2f;
    private float speed = 2f;
    private float attackTimer = 1.5f;


    private int phase = 1;
    private int dir = 2;
    private int bounces = 0;

    public Rigidbody2D minion;
    private bossAI Boss;

    //I use this bool to make sure the projectile original is not moving
    public bool isActive = false;

    // Use this for initialization
    void Start()
    {
        //Reference to boss is used to change speed as fight goes on
        Boss = GameObject.Find("Boss").GetComponent<bossAI>();
    }

    void Update()
    {
        if (!static_information.isPaused)
        {
            print(isActive);
            if (isActive)
            {

                //Checks to see if bounced 3 times
                if (bounces >= 3)
                    spawnMinion();

                //Checks if own health is 0
                if (health == 0)
                    Destroy(this.gameObject);

                //Chekcs if Boss' health is 0
                if (Boss.health == 0)
                    Destroy(this.gameObject);


                if (attackTimer > 0)
                    attackTimer -= Time.deltaTime;


                if (attackTimer <= 0)
                    ; //Deal damage if touching bard

                speed = baseSpeed + (Boss.maxHealth - Boss.health) / 75f;
                AttemptMove(movementAI());
            }
        }
    }
    public float[] movementAI()
    {
        float yDir;
        float xDir;
        if (dir == 5)
            dir = 1;

        if (dir == 1 || dir == 2)
            yDir = 1f * speed;
        else
            yDir = -1f * speed;
        if (dir == 1 || dir == 4)
            xDir = 1f * speed;
        else
            xDir = -1f * speed;


        float[] movement = { xDir, yDir };

        return movement;
    }
    public void AttemptMove(float[] movement)
    {

        Vector2 new_position = new Vector2(transform.position.x + movement[0], transform.position.y + movement[1]);

        //Checks to see if out of bounds and changes direction based on that
        if (new_position[0] < 20)
        {
            if (dir == 2)
                dir = 1;
            else
                dir = 4;
            bounces++;
            return;
        }
        //I used an offset (20 in this case and 50 later) so that the sprite 
        //would not clip outside of the boss room

        //***Offset should be changed depending on the radius of the sprite***
        if (new_position[0] > Camera.main.pixelWidth - 20)
        {
            if (dir == 1)
                dir = 2;
            else
                dir = 3;
            bounces++;
            return;
        }
        if (new_position[1] < 50)
        {
            if (dir == 4)
                dir = 1;
            else
                dir = 2;
            return;
        }
        if (new_position[1] > Camera.main.pixelHeight - 50)
        {
            if (dir == 1)
                dir = 4;
            else
                dir = 3;
            bounces++;
            return;
        }


        transform.position = new_position;
    }

    public void hit(int damage)
    {
        health -= damage;
    }

    public void spawnMinion()
    {
        print("Spawning Minion");
        Rigidbody2D minionClone = (Rigidbody2D)Instantiate(minion, transform.position, transform.rotation);
        minionClone.GetComponent<bossMinion>().isActive = true;
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            //take damage and spawn minion
            spawnMinion();
        }

    }
}
