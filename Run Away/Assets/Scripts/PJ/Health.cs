using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	Rigidbody2D playerRB;

	public int playerHealth = 1;
	public int enemyDemage =1;
	// Use this for initialization
	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is alled once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemigo") {
			playerHealth = enemyDemage - playerHealth;
			if (playerHealth <= 0) {
				Destroy (gameObject);
			}

		}
	}
}
