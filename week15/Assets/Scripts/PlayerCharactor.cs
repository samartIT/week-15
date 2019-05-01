using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharactor : MonoBehaviour {
	private int _health;

	//void Start () {
	//	_health = 5;	
	//}

	public void Hurt(int damage) {
        Managers.Player.ChangeHealth(-damage);
		//_health -= damage;
		//Debug.Log("Health " + _health);
	}
}
