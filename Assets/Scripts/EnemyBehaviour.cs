﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150;

	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log(collider);
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			//Debug.Log("Hit by a projectile");
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
