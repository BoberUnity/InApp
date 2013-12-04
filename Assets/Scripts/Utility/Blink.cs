using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	Transform t;

	public bool isEnabled = true; 
	public bool defaultState = true;
	public float enableTime = 1.0f;
	public float disableTime = 0.5f;

	private bool currentState = true;

	void Toggle() {
		currentState = !currentState;
		Invoke("Toggle", currentState ? enableTime : disableTime);
		t.gameObject.SetActive(currentState);
	}
	
	void Awake () {
		t = transform;
		Toggle();
	}
	
}
