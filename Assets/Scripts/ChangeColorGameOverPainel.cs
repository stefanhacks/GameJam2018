using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorGameOverPainel : MonoBehaviour {

	private Image painelGameOver;

	void Start () {
		painelGameOver = GetComponent<Image> ();
		//InvokeRepeating("TrocarCor", 0.2f, 0.4f);

	}

	
	void Update () {
		
	}

	void TrocarCor () {
		Color newColor = new Color (Random.value, 0, Random.value, 1.0f);
		painelGameOver.color = newColor;

	}
}
