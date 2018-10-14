using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultEnemy : MonoBehaviour {

	private Vector3 enemyPosition;
	public float moveSpeed = 0.1f;
	public float damageDealt = 1f;
	public float health = 10;

	// public defaultEnemy(float moveSpeed, float damage, float health){
	// 	this.moveSpeed = moveSpeed;
	// 	this.damageDealt = damage;
	// 	this. health = health;

	// }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FollowMouse();
	}

	void FollowMouse () {
		//if(Input.GetMouseButton(1)){

		enemyPosition = Input.mousePosition;
		enemyPosition = Camera.main.ScreenToWorldPoint(enemyPosition);
		transform.position = Vector2.Lerp(transform.position, enemyPosition, moveSpeed);
		//}
	}
}
