using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarBehaviour : MonoBehaviour {

	public float speed = 1f;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = transform;
	}

	// Update is called once per frame
	void Update () {
		t.Translate(0, 0, -speed * Time.deltaTime);
		if (t.position.z > 29) t.position = new Vector3(t.position.x, 0, -24);
		if (t.position.z < -25) t.position = new Vector3(t.position.x, 0, 28);
	}

}
