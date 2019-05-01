using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
	[SerializeField] private string itemName;

	void OnTriggerEnter(Collider other) {
		//		Debug.Log ("item collected = " + itemName);	
		Managers.Inventory.AddItem(itemName);
		Destroy (this.gameObject);
	}
}
