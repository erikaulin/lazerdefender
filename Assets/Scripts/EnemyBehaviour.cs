using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log(collider);
	}
}
