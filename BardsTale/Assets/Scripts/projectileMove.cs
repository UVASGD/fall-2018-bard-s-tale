using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour {

    public float speed;
    public float damage = 10;
    public GameObject castFireball, leftFireball, rightFireball, upFireball, downFireball;
    public LayerMask whatToHit;

    Animator anim;
    public string casting;
    private Transform firePoint;

    private float timeToFire = 0;


    // Use this for initialization
    void Start() { 
        anim = GameObject.Find("castFireball").GetComponent<Animator>();
        castFireball.SetActive(true);
        firePoint = transform.Find("firePoint");
        if (firePoint == null) {
            Debug.LogError("No firePoint");
        }

    }

    // Update is called once per frame
    void Update() {
        Fire();


        /*if (speed != 0) {
            transform.position += transform.forward * (speed * Time.deltaTime);
        } else {
            Debug.Log("No Speed");
        }
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                Shoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        } */
    }

    IEnumerator delay(float time, GameObject fireball)
    {
        yield return new WaitForSeconds(time);
        Instantiate(fireball);
    }

    void Shoot() {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.red);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.yellow);
        }
    }

    void Fire()
    {
        Vector2 playerPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        float time = Time.time;
        if (Input.GetButtonDown("up"))
        {
            anim.SetTrigger(casting);
            upFireball.transform.position = playerPosition;
            StartCoroutine(delay(.5f, upFireball));
        }
        else if (Input.GetButtonDown("right"))
        {
            anim.SetTrigger(casting);
            rightFireball.transform.position = playerPosition;
            StartCoroutine(delay(.5f, rightFireball));
        }
        else if (Input.GetButtonDown("down"))
        {
            anim.SetTrigger(casting);
            downFireball.transform.position = playerPosition;
            StartCoroutine(delay(.5f, downFireball));
        }
        else if (Input.GetButtonDown("left"))
        {
            anim.SetTrigger(casting);
            leftFireball.transform.position = playerPosition;
            StartCoroutine(delay(.5f, leftFireball));
        }
    }
}