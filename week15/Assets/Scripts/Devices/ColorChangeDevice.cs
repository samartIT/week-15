using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour {
	public void Operate() {
		float R = Random.Range (0f, 1f);
		float G = Random.Range (0f, 1f);
		float B = Random.Range (0f, 1f);
		Color random = new Color (R, G, B);
		GetComponent<Renderer> ().material.color = random;
	}
}

