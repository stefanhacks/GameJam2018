using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private GameObject[] player, plataformas;
	private GameObject chaveSlot, final;
	private AudioSource audioManager;
	public AudioClip music;
	public GameObject textoSlow, painelGameOver, textoGameOver, textoWin, textoScore;
	public float tempoSlow = 0, tempoMorte = 0, tempoFinalizar;
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
		chaveSlot = GameObject.FindGameObjectWithTag ("ChaveSlot");
		final = GameObject.FindGameObjectWithTag ("Final");
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
				chaveSlot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = chaveImg;
			} else if (gS.quantidadeChave == 2) {
				chaveSlot.transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = chaveImg;
			} else if (gS.quantidadeChave == 3) {
				chaveSlot.transform.GetChild(2).GetComponent<SpriteRenderer> ().sprite = chaveImg;
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
				gS.venceu = true;
			}
		} else if (currentState == GameState.GameOver) {
			gS.pontuacaoFinal = (gS.tempoJogo * 100 + gS.alturaPlayers * 200 + gS.quantidadeChave * 300) / 3;
			textoScore.GetComponent<Text> ().text = ("Score:\n" + gS.pontuacaoFinal.ToString("f0"));
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
			player [0].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
			player [1].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
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
			Vector2 distanciaP1 = new Vector2 (0, plataformas.transform.position.y - final.transform.position.y);
			Vector2 distanciaP2 = new Vector2 (0, plataformas.transform.position.y - final.transform.position.y);
			if (distanciaP1.y <= 2 || distanciaP2.y <=2) {
				Destroy (plataformas);
			}
		}
		gS.velocidadeParedeMeio = 0.075f;
		tempoFinalizar += Time.deltaTime;

		if (tempoFinalizar < 4) {
			final.transform.Translate (0, gS.velocidadeParedeMeio / 4, 0);
		}

		if (tempoFinalizar >= 4){
			gS.moveCamera = false;
		}

		if (gS.fim) {
			textoGameOver.SetActive (false);
			textoWin.SetActive (true);
			painelGameOver.SetActive (true);
			currentState = GameState.GameOver;
		}

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
