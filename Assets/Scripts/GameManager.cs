using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	private GameObject[] player, plataformas;
	public GameObject textoSlow, painelGameOver, textoGameOver, textoWin, textoScore, chaveSlot, final, highFive;
	private float tempoSlow = 3.5f, tempoMorte = 0, tempoFinalizar, tempoJogo, alturaPlayers, pontuacaoFinal, tempoFinal;
	private bool morreu = false;
	private GameSettings gS;
	private MainCameraController mCC;

	public enum GameState
	{
		Readying,
		Playing,
		GameOver
	}

	public GameState currentState = GameState.Readying;
	private AudioSource audioManager, audioSource;
	private AudioClip deathSound, victorySound, music;
	private Text textoTempo, textoMetros;
	private Sprite chaveImg;

	public bool venceu = false, fim = false, comecou = false;
	public int quantidadeChave;

	void Start ()
	{

		//Loading Resources
		deathSound = Resources.Load<AudioClip> ("Musics/TopsToques-NaveEspacial8bitEfeitoSonoro");
		victorySound = Resources.Load<AudioClip> ("Musics/MyInstants-HellYeah");
		music = Resources.Load<AudioClip> ("Musics/DarkLight-Vodovoz");
		chaveImg = Resources.Load <Sprite> ("Graphics/Images/Key");

		//Finding References
		player = GameObject.FindGameObjectsWithTag ("Player");
		chaveSlot = GameObject.FindGameObjectWithTag ("ChaveSlot");
		highFive = GameObject.FindGameObjectWithTag ("High5");
		final = GameObject.FindGameObjectWithTag ("Final");
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		mCC = GameObject.Find ("MainCamera").GetComponent<MainCameraController> ();
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioSource> ();
		audioSource = this.GetComponent<AudioSource> ();
		audioManager.clip = music;
		textoTempo = GameObject.Find ("Canvas/TextoTempo").GetComponent<Text> ();
		textoMetros = GameObject.Find ("Canvas/TextoMetros").GetComponent<Text> ();
		textoSlow = GameObject.Find ("Canvas/TextoTempoSlow");
		painelGameOver = GameObject.Find ("Canvas/PainelGameOver");
		textoGameOver = GameObject.Find ("Canvas/PainelGameOver/TextoGameOver");
		textoWin = GameObject.Find ("Canvas/PainelGameOver/TextoVitoria");
		textoScore = GameObject.Find ("Canvas/PainelGameOver/TextoScore");

		painelGameOver.SetActive (false);
		textoWin.SetActive (false);
	}

	void Update ()
	{

		textoTempo.text = (tempoJogo.ToString ("0" + " s"));
		textoMetros.text = (alturaPlayers.ToString ("0" + " m"));

		//CHEAT TO WIN
		if (Input.GetKeyDown (KeyCode.K)) {
			quantidadeChave = 3;
		}


		if (currentState == GameState.Readying) {
			comecou = true;
			Slow ();
		} else if (currentState == GameState.Playing) {
			checarAltura ();
			if (quantidadeChave == 1) {
				chaveSlot.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = chaveImg;
			} else if (quantidadeChave == 2) {
				chaveSlot.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = chaveImg;
				chaveSlot.transform.GetChild (1).GetComponent<SpriteRenderer> ().sprite = chaveImg;
			} else if (quantidadeChave >= 3) {
				chaveSlot.transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = chaveImg;
				chaveSlot.transform.GetChild (1).GetComponent<SpriteRenderer> ().sprite = chaveImg;
				chaveSlot.transform.GetChild (2).GetComponent<SpriteRenderer> ().sprite = chaveImg;
			}

			plataformas = GameObject.FindGameObjectsWithTag ("Plataforma");
			if (!venceu) {
				checkDeath (player [0]);
				checkDeath (player [1]);
			}
			if (mCC.moveCamera) {
				tempoJogo += Time.deltaTime;
			}

			if (quantidadeChave >= 3) {
				Vencer ();
				venceu = true;
			}
		} else if (currentState == GameState.GameOver) {
			pontuacaoFinal = (tempoJogo * 100 + alturaPlayers * 200 + quantidadeChave * 300) / 3;
			textoScore.GetComponent<Text> ().text = ("Score:\n" + pontuacaoFinal.ToString ("f0"));
		}
	}

	void Slow ()
	{
		if (tempoSlow > 1) {
			Time.timeScale = 0.1f;
			textoSlow.SetActive (true);
			textoSlow.GetComponent<Text> ().text = "" + tempoSlow.ToString ("f0");
		}

		if (tempoSlow <= 0 && tempoSlow >= -1) {
			textoSlow.GetComponent<Text> ().text = "GO";
		}

		if (tempoSlow < -1) {
			Time.timeScale = 1;
			textoSlow.SetActive (false);
			currentState = GameState.Playing;
			player [0].GetComponent<PlayerController> ().movementEnabled = true;
			player [1].GetComponent<PlayerController> ().movementEnabled = true;
			audioManager.Play ();
		}
	
		tempoSlow -= Time.deltaTime * 10;

	}

	void checkDeath (GameObject play)
	{
		if (morreu) {
			tempoMorte += Time.deltaTime;
		}

		if (tempoMorte >= 3) {
			textoWin.SetActive (false);
			textoGameOver.SetActive (true);
			painelGameOver.SetActive (true);
			currentState = GameState.GameOver;
		}
		if (play != null) {
			if (Blitzkrieg.GetGameObjectPosition (play).y < 0 || Blitzkrieg.GetGameObjectPosition (play).y > 1) {
				morreu = true;
				if (mCC.moveCamera) {
					play.transform.GetChild (0).gameObject.SetActive (true);

					audioSource.clip = deathSound;
					audioSource.Play ();
				}
				mCC.moveCamera = false;
				player [0].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				player [1].GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				player [0].GetComponent<PlayerController> ().movementEnabled = false;
				player [1].GetComponent<PlayerController> ().movementEnabled = false;
				foreach (GameObject plataformas in plataformas) {
					if (plataformas.GetComponent<SpriteRenderer> ().enabled == false) {
						plataformas.GetComponent<SpriteRenderer> ().enabled = true;
						plataformas.GetComponent<ColorChange> ().enabled = true;
					}
				}
			}
		}
	}

	public void Vencer ()
	{
		foreach (GameObject plataformas in plataformas) {
			Vector2 distanciaP1 = new Vector2 (0, plataformas.transform.position.y - final.transform.position.y);
			Vector2 distanciaP2 = new Vector2 (0, plataformas.transform.position.y - final.transform.position.y);
			if (distanciaP1.y <= 2 || distanciaP2.y <= 2) {
				Destroy (plataformas);
			}
		}
		tempoFinal = 0.068f;
		tempoFinalizar += Time.deltaTime;

		if (tempoFinalizar < 4) {
			final.transform.Translate (0, tempoFinal / 4, 0);
		}

		if (tempoFinalizar >= 4) {
			mCC.moveCamera = false;
		}

		if (fim) {
			highFive.GetComponent<Animator> ().Play ("Hi5");
		}
	}

	public void proceedGameWin ()
	{
		textoGameOver.SetActive (false);
		textoWin.SetActive (true);
		painelGameOver.SetActive (true);
		currentState = GameState.GameOver;
		audioSource.clip = victorySound;
		audioSource.Play ();
	}

	public void buttonPressed (KeyCode keyPressed)
	{
		if (keyPressed == KeyCode.Space && currentState == GameState.GameOver) {
			audioManager.Stop ();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if (keyPressed == KeyCode.Escape && currentState == GameState.GameOver) {
			audioManager.Stop ();
			Destroy (audioManager.gameObject);
			SceneManager.LoadScene (0);
		}
	}

	public void checarAltura ()
	{
		if (player [0] != null && player [1] != null) {
			if (player [0].transform.position.y > player [1].transform.position.y) {
				alturaPlayers = (player [1].transform.position.y - player [1].GetComponent<PlayerController> ().posicaoPlayerInicial.y) * -1;
			} else {
				alturaPlayers = (player [0].transform.position.y - player [0].GetComponent<PlayerController> ().posicaoPlayerInicial.y) * -1;
			}
		}
	}
}
