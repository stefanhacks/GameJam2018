using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	public enum PlayerType
	{
		PlayerOne,
		PlayerTwo
	}

	public PlayerType currentPlayer = PlayerType.PlayerOne;

	private Rigidbody2D rb;
	private Animator animator;
	public bool movementEnabled = false;
	private float moveHorizontal;
	private bool ladoD = true;
	private GameManager gM;
	private GameSettings gS;
	private GameObject[] chaves;
	private AudioClip keySound;
	private GameObject tiro1, tiro2;

	public Vector3 posicaoPlayerInicial;

	void Start ()
	{
		//Loading Resources
		keySound = Resources.Load<AudioClip> ("Musics/PickKey-Shnur");

		//Finding References
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		gM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		posicaoPlayerInicial.y = transform.position.y;

		tiro1 = Resources.Load ("Prefabs/Tiro1") as GameObject;
		tiro2 = Resources.Load ("Prefabs/Tiro2") as GameObject;
	}

	void Update ()
	{
		Movimentacao ();
		Atira ();
		Animations ();
	


	}

	void FixedUpdate ()
	{
		rb.velocity = new Vector2 (moveHorizontal * gS.velocidadePlayers, 0);
	}

	private void Movimentacao(){
		if (currentPlayer == PlayerType.PlayerOne && movementEnabled) {
			moveHorizontal = Input.GetAxis ("HorizontalP1");

		} else if (movementEnabled) {
			moveHorizontal = Input.GetAxis ("HorizontalP2");
		}

		Flip (moveHorizontal);

	}

	private void Atira(){
		
		gS.cdTiroP1 += Time.deltaTime;
		gS.cdTiroP2 += Time.deltaTime;

		if (currentPlayer == PlayerType.PlayerOne && movementEnabled && gS.cdTiroP1 > 2f) {
			if (Input.GetButtonDown("Fire1")){
				GameObject tiro1Clone = Instantiate(tiro1, new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation) as GameObject;
				tiro1Clone.GetComponent<Tiro>().SetInitator(this.gameObject);
				tiro1Clone.GetComponent<Rigidbody2D>().velocity = new Vector2 (gS.velTiro*-1, 0);
				gS.cdTiroP1 = 0f;
			}

		} else if (movementEnabled && gS.cdTiroP2 > 2f) {
			if (Input.GetButtonDown("Fire2")){
				GameObject tiro2Clone = Instantiate(tiro2, new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
				tiro2Clone.GetComponent<Tiro>().SetInitator(this.gameObject);
				tiro2Clone.GetComponent<Rigidbody2D>().velocity = new Vector2 (gS.velTiro, 0);
				gS.cdTiroP2 = 0f;
			}
		}
	}

	private void Animations(){
		if (rb.velocity.x < -0.1f || rb.velocity.x > 0.1f) {
			animator.SetBool ("andando", true);
		} else {
			animator.SetBool ("andando", false);
		}			
	}

	private void Flip (float horizontal)
	{
		if (horizontal > 0 && !ladoD || horizontal < 0 && ladoD) {
			ladoD = !ladoD;
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y * 1, transform.localScale.z * 1);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Chave") {
			chaves = GameObject.FindGameObjectsWithTag ("Chave");
			foreach (GameObject chaves in chaves) {
				Destroy (chaves);
			}
			gM.quantidadeChave++;

			AudioSource audioSource = this.GetComponent<AudioSource> ();
			audioSource.clip = keySound;
			audioSource.Play ();
		}

		if (col.gameObject.tag == "Porta") {
			gM.fim = true;
		}
	}
}
