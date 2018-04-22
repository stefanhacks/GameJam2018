using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;

	public enum PlayerType {
		PlayerOne,
		PlayerTwo
	}

	public AudioClip keySound, deathSound;
	public PlayerType currentPlayer = PlayerType.PlayerOne;
	public bool movementEnabled = false;

	private float moveHorizontal;
	private bool ladoD = true;
	private GameSettings gS;
	private Animator animator;

	private GameObject[] chaves;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		gS.posicaoPlayerInicial.y = transform.position.y;
	}

	void Update () {
		if (currentPlayer == PlayerType.PlayerOne && movementEnabled) {
			moveHorizontal = Input.GetAxis ("HorizontalP1");

		} else if (movementEnabled) {
			moveHorizontal = Input.GetAxis ("HorizontalP2");
		}
		Flip (moveHorizontal);

		if (rb.velocity.x < -0.1f || rb.velocity.x > 0.1f) {
			animator.SetBool ("andando", true);
		} else {
			animator.SetBool ("andando", false);
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector2 (moveHorizontal * gS.velocidadePlayers, 0);
	}

	private void Flip (float horizontal) {
		if (horizontal > 0 && !ladoD || horizontal < 0 && ladoD) {
			TrocarDirecao ();
		}
	}

	public void TrocarDirecao () {
		ladoD = !ladoD;
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y * 1, transform.localScale.z * 1);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Chave") {
			chaves = GameObject.FindGameObjectsWithTag ("Chave");
			foreach (GameObject chaves in chaves) {
				Destroy(chaves);
			}
			gS.quantidadeChave++;

			AudioSource audioSource = this.GetComponent<AudioSource> ();
			audioSource.clip = keySound;
			audioSource.Play ();
		}

		if (col.gameObject.tag == "Porta") {
			gS.fim = true;
		}
	}
}
