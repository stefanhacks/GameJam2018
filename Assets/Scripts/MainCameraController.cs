using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{

	private GameObject[] player;
	private GameSettings gS;

	public bool moveCamera = true;

	void Start ()
	{
		//Finding References
		player = GameObject.FindGameObjectsWithTag ("Player");	
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
	}

	void Update ()
	{
		if (moveCamera) {
			MoveCamera ();
		}
	}

	void MoveCamera ()
	{
		gS.velocidadeCamera = -1 * Time.deltaTime;

		if (player [0] != null && player [1] != null) {
			Vector2 distancia = new Vector2 (0, player [0].transform.position.y - player [1].transform.position.y);
			if (distancia.y > -3 && distancia.y < 3) {
				transform.Translate (0, gS.velocidadeCamera * 2f, 0);
			} 
		}else {
			transform.Translate (0, gS.velocidadeCamera, 0);
		}
	}
}
