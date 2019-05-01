using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingAI : MonoBehaviour {
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private bool _alive;

	void Start () {
		_alive = true;	// set live state
	}

	void Update () {
		if (_alive) {	// check if enemy still alive, then move
			transform.Translate (0, 0, speed * Time.deltaTime);
		}
		// set ray-direction must be infront of enemy
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.SphereCast(ray, 0.75f, out hit)){
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.GetComponent<PlayerCharactor>()) {
				if(_fireball == null){
					_fireball = Instantiate (fireballPrefab) as GameObject;
					_fireball.transform.position = transform.TransformPoint (Vector3.forward*1.5f);
					_fireball.transform.rotation = transform.rotation;
				}
			}
			else if (hit.distance < obstacleRange) { // check the distance to turn
				float angle = Random.Range (-110, 110);
				transform.Rotate (0, angle, 0);
			}
		}
	}

	// allow enemy alive state to be set
	public void SetAlive(bool aliveState) {
		_alive = aliveState;
	}
}
