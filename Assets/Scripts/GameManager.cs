using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private GameObject[] player, plataformas, chaveSlot;
	private AudioSource audioManager;
	public AudioClip music;
	public GameObject textoSlow, painelGameOver, plataformVitoriaP1, plataformVitoriaP2, textoGameOver, textoWin;
	public float tempoSlow = 0, tempoMorte = 0;
	public Text textoTempoSlow;

	public Sprite chaveImg;

	public enum GameState {
		Readying,
		Playing,
		GameOver
	}

	public GameState currentState = GameState.Readying;
	public bool comecou = false;
	public GameObject[] paredes;
	private GameSettings gS;

	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player");
		paredes = GameObject.FindGameObjectsWithTag ("Paredes");
		chaveSlot = GameObject.FindGameObjectsWithTag ("ChaveSlot");
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioSource> ();
		audioManager.clip = music;
	}


	void Update () {
		if (currentState == GameState.Readying) {
			Slow ();
        } else if (currentState == GameState.Playing)
        {
			if (gS.quantidadeChave == 1) {
				chaveSlot [0].GetComponent<SpriteRenderer> ().sprite = chaveImg;
			} else if (gS.quantidadeChave == 2) {
				chaveSlot [1].GetComponent<SpriteRenderer> ().sprite = chaveImg;
			} else if (gS.quantidadeChave == 3) {
				chaveSlot [2].GetComponent<SpriteRenderer> ().sprite = chaveImg;
			}

			plataformas = GameObject.FindGameObjectsWithTag("Plataforma");
            checkDeath(player[0]);
            checkDeath(player[1]);

			if (gS.moveCamera) {
				gS.tempoJogo += Time.deltaTime;
				for (int i = 0; i < paredes.Length; i++) {
					paredes[i].transform.Translate (0, gS.velocidadeParedeMeio / 4, 0);
				}
			}

			if (gS.quantidadeChave >=3) {
				Vencer ();
			}
		} else if (currentState == GameState.GameOver) {
			
		}
	}

	void Slow () {
		tempoSlow -= Time.deltaTime * 10;

		if (tempoSlow > 1) {
			Time.timeScale = 0.1f;
			textoSlow.SetActive (true);
			textoTempoSlow.text = "" + tempoSlow.ToString ("f0");

		}

		if (tempoSlow <= 0 && tempoSlow >= -1) {
			textoTempoSlow.text = "GO";
			;
		}

		if (tempoSlow < -1) {
			Time.timeScale = 1;
			textoSlow.SetActive (false);
			currentState = GameState.Playing;
			player[0].GetComponent<PlayerController> ().movementEnabled = true;
			player[1].GetComponent<PlayerController> ().movementEnabled = true;
			audioManager.Play ();
		}
	}

	void checkDeath (GameObject play) {
		
		if (Blitzkrieg.GetGameObjectPosition (play).y <= -0.15 || Blitzkrieg.GetGameObjectPosition (play).y > 1.10) {

			tempoMorte += Time.deltaTime;
			gS.moveCamera = false;
			player[0].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			player[1].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			player[0].GetComponent<PlayerController> ().movementEnabled = false;
			player[1].GetComponent<PlayerController> ().movementEnabled = false;
			foreach (GameObject plataformas in plataformas) {
				if (plataformas.GetComponent<SpriteRenderer> ().enabled == false) {
					plataformas.GetComponent<SpriteRenderer> ().enabled = true;
					plataformas.GetComponent<ColorChange> ().enabled = true;
				}
			}
			if (tempoMorte >= 3) {
				textoWin.SetActive (false);
				textoGameOver.SetActive (true);
				painelGameOver.SetActive (true);
				currentState = GameState.GameOver;
			}
		}
	}

	void Vencer () {
		foreach (GameObject plataformas in plataformas) {
			Vector2 distanciaP1 = new Vector2 (0, plataformas.transform.position.y - plataformVitoriaP1.transform.position.y);
			Vector2 distanciaP2 = new Vector2 (0, plataformas.transform.position.y - plataformVitoriaP2.transform.position.y);
			if (distanciaP1.y <= 2 || distanciaP2.y <=2) {
				Destroy (plataformas);
			}
		}
		textoGameOver.SetActive (false);
		textoWin.SetActive (true);
		plataformVitoriaP1.gameObject.SetActive (true);
		plataformVitoriaP2.gameObject.SetActive (true);
		gS.moveCamera = false;
		currentState = GameState.GameOver;
	}

	public void buttonPressed (KeyCode keyPressed) {
		if (keyPressed == KeyCode.Space && currentState == GameState.GameOver) {
			SceneManager.LoadScene (1);
		}
		if (keyPressed == KeyCode.Escape && currentState == GameState.GameOver) {
			SceneManager.LoadScene (0);
		}
	}
}
