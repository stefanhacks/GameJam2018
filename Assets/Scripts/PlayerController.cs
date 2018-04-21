using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int velocidade;
	Rigidbody2D rb;

	public enum PlayerType { PlayerOne, PlayerTwo };
	public PlayerType currentPlayer = PlayerType.PlayerOne;

	private float moveHorizontal; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

	}

	void Update(){
		if (currentPlayer == PlayerType.PlayerOne) {
			moveHorizontal = Input.GetAxis ("HorizontalP1");
		} else {
			moveHorizontal = Input.GetAxis ("HorizontalP2");
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector2(moveHorizontal * velocidade, 0);
	}
}
