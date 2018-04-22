using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

	public GameObject[] player;

	private GameSettings gS;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player");	
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gS.moveCamera) {
			MoveCamera ();
		}

		/*
		if (player [1].transform.position.y <= transform.position.y) {
			transform.Translate (0, player [1].transform.position.y * Time.deltaTime / 10, 0);
		} 

		if (player [0].transform.position.y <= transform.position.y) {
			transform.Translate (0, player [0].transform.position.y * Time.deltaTime / 10, 0);
		} 
		*/
	}

	void MoveCamera () {
		Vector2 distancia = new Vector2 (0, player[0].transform.position.y - player[1].transform.position.y);

		gS.velocidadeCamera = -1 * Time.deltaTime;

		if (distancia.y > -3 && distancia.y < 3) {
			transform.Translate (0, gS.velocidadeCamera * 2f, 0);
		} else {
			transform.Translate (0, gS.velocidadeCamera, 0);
		}
	}
}
