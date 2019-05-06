using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {
	public float radius = 1.5f;

	void Update () {
		if (Input.GetButtonDown ("Fire3")) { // Left-Shift Key
			Collider[] hitColliders = Physics.OverlapSphere (transform.position, radius);
			foreach (Collider hitCollider in hitColliders) {
				Vector3 direction = hitCollider.transform.position - transform.position;
				// see if player faces a wall
				if (Vector3.Dot (transform.forward, direction) > 0.5f) {
					Debug.Log (hitCollider.transform.position + ":" + transform.position + ":" + transform.forward);
					hitCollider.SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);
				}
				Debug.Log (Vector3.Dot (transform.forward, direction));
			}
		}
	}
}


