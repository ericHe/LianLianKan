using UnityEngine;
using System.Collections;

public class RocketTrail : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
	}
}
