﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject laser;
	public GameObject EnemyShot;
	public GameObject explode;
	public float shotFadeTime = 0.1f;
	public float destoyedFadeTime = 0.4f;
	public float projectileSpeed = 10;
	public float projectileRepeatRate = 0.2f;
	public float health = 250;
	public float speed = 15.0f;
	public float padding = 1;
	public AudioClip playerFire;
	public AudioClip playerShields;
	public AudioClip playerDies;
	
	private float xmax = -5;
	private float xmin = 5;
	
	void Start (){
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x	- padding;
	}
	
	void Fire () {
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(laser, transform.position+offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint (playerFire, transform.position, 0.4f);
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating ("Fire", 0.0001f, projectileRepeatRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax),
				 transform.position.y,
				 transform.position.z
			);
		}else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax),
				transform.position.y,
				transform.position.z
			);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log(collider);
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			//Debug.Log("Hit by a projectile");
			health -= missile.GetDamage();
			missile.Hit();
			AudioSource.PlayClipAtPoint (playerShields, transform.position, 0.4f);
			Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Hit () {
		GameObject clone = (GameObject)Instantiate (EnemyShot, transform.position, Quaternion.identity);
		Destroy (clone, shotFadeTime);
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint (playerDies, transform.position, 0.4f);
		GameObject destoryed = (GameObject)Instantiate (explode, transform.position, Quaternion.identity);
		Destroy (destoryed, destoyedFadeTime);
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}


