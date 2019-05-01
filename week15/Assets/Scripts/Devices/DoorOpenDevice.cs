using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour {
	[SerializeField] private Vector3 dPos;
	private bool _open; // default is false;

	public void Operate(){
		if (_open) {	// If a door is open, then close it
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
		} else {		// If a door is closed, then open it
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
		}
		_open = !_open;
	}

	public void Activate(){
		Debug.Log ("Activate");
		if (!_open) {
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
			_open = true;
		}
	}
	public void Deactivate(){
		Debug.Log ("Deactivate");
		if (_open) {
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
			_open = false;
		}
	}

}

