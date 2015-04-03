using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1.0f;
	
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += new Vector3(-speed, 0, 0);
		}else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += new Vector3(+speed, 0,0);
		//}else if (Input.GetKey(KeyCode.UpArrow)){
		//	transform.position += new Vector3(-speed, 0,0);
		//}else if (Input.GetKey(KeyCode.DownArrow)){
		//	transform.position += new Vector3(+speed, 0,0);
		}
	}
}
