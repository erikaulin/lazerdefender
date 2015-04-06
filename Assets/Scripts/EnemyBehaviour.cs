using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float projectileSpeed = 10;
	public GameObject projectile;
	public GameObject PlayerShot;
	public GameObject explode;
	public float shotFadeTime = 0.1f;
	public float destoyedFadeTime = 0.4f;
	public float health = 150;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	public AudioClip enemyFire;
	public AudioClip enemyDies;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update (){
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability){
			Fire ();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0,-1,0); 
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint (enemyFire, transform.position, 0.4f);
	}

	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log(collider);
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			//Debug.Log("Hit by a projectile");
			health -= missile.GetDamage();
			missile.Hit();
			Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Hit () {
		GameObject clone = (GameObject)Instantiate (PlayerShot, transform.position, Quaternion.identity);
		Destroy (clone, shotFadeTime);
	}
	
	void Die () {
		AudioSource.PlayClipAtPoint (enemyDies, transform.position, 0.4f);
		GameObject destoryed = (GameObject)Instantiate (explode, transform.position, Quaternion.identity);
		Destroy (destoryed, destoyedFadeTime);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
	
}
