using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

	public GameObject[] player;

	public float transicao, transicao2;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player");	
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 distancia = new Vector2 (0, player[0].transform.position.y - player[1].transform.position.y);

		Debug.Log ("distancia: " + distancia);
		transicao = -2 * Time.deltaTime;
		transicao2 = -4 * Time.deltaTime;

		if (distancia.y > -2 && distancia.y < 2) {
			transform.Translate (0, transicao, 0);
		} else {
			transform.Translate (0, transicao2, 0);
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
}
