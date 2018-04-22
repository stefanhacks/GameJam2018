using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorGameOverText : MonoBehaviour {

	private Text gameOverText;

	void Start () {
		gameOverText = GetComponent<Text> ();
		InvokeRepeating ("TrocarCor", 0.2f, 0.1f);
	}

	
	void Update () {
		
	}

	void TrocarCor () {
		Color newColor = new Color (Random.value, Random.value, Random.value, 1.0f);
		gameOverText.color = newColor;
	}
}
