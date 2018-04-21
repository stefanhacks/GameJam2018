using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

	private SpriteRenderer sR;


	// Use this for initialization
	void Start () {
		sR = GetComponent<SpriteRenderer> ();
		InvokeRepeating("TrocarCor", 0.5f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void TrocarCor(){
		Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
		sR.color = newColor;

	}

}
