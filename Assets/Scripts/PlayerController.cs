﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject laser;
	public float projectileSpeed = 10;
	public float projectileRepeatRate = 0.2f;
	
	public float speed = 15.0f;
	public float padding = 1; 
	
	private float xmax = -5;
	private float xmin = 5;
	
	void Start (){
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x	- padding;
	}
	
	void Fire () {
		GameObject beam = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed,0);
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
		//}else if (Input.GetKey(KeyCode.UpArrow)){
		//	transform.position += new Vector3(-speed, 0,0);
		//}else if (Input.GetKey(KeyCode.DownArrow)){
		//	transform.position += new Vector3(+speed, 0,0);
		}
	}
}


