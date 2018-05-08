using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour {
	
	public GameObject pai, gM;

	public void SetInitator(GameObject theObject){
		pai = theObject;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != pai) {
			if (other.gameObject.tag == "Player") {
				gM = GameObject.FindGameObjectWithTag ("GameManager");
				gM.GetComponent<GameManager> ().quantidadeChave = 3;
				Destroy (other.gameObject);
				Destroy (this.gameObject);
			}
		}

	}

	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
