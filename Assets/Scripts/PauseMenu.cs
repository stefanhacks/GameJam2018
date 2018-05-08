using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	private GameObject menu, tempo;
	private bool paused = false, despausou = false;
	private float tempoVolta = 1;
	private GameManager gM;

	public List<GameObject> botoes;
	private GameObject criado, icone, canvas;
	private Transform botao;

	public int opcoes = 0, posicao = 1;

	void Start ()
	{
		menu = GameObject.Find ("Canvas/PauseMenuManager/MenuPanel");
		tempo = GameObject.Find ("Canvas/PauseMenuManager/TempoVolta");
		icone = GameObject.Find ("Canvas/PauseMenuManager/MenuPanel/IconeMenu");
		icone.transform.localScale = new Vector2 (100, 100);
		gM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		menu.SetActive (false);
		tempo.SetActive (false);

		canvas = GameObject.Find ("Canvas/PauseMenuManager");
		botao = canvas.transform.Find ("MenuPanel");

		for (int i = 0; i < botao.childCount; i++){
			GameObject filhoBotao = botao.transform.GetChild (i).gameObject;
			botoes.Add(filhoBotao);
			opcoes++;
		}
	}



	void Update ()
	{

		if (gM.currentState == GameManager.GameState.Playing) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				paused = !paused;
				if (!paused) {
					despausou = true;
				}
			}

			if (paused) {
				menu.SetActive (true);
				tempo.SetActive (false);
				Time.timeScale = 0;
				tempoVolta = 0.6f;
				AudioListener.pause = true;
				PosicoesMenu ();
			}

			if (!paused) {
				menu.SetActive (false);
				AudioListener.pause = false;
			}

			if (despausou && !paused) {
				Volta ();
			}
		}
	}

	void PosicoesMenu(){

		for (int i = 0; i < posicao; i++) {
			icone.transform.position = botoes [i].transform.position;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			if (posicao < opcoes -2)
			{
				posicao++;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			if (posicao > 1)
			{
				posicao--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			for (int i = 0; i < posicao; i++) {
				botao.transform.GetChild (posicao-1).GetComponent<Button> ().onClick.Invoke ();
			}
		}
	}

	void Volta ()
	{
		tempoVolta += Time.deltaTime * 10;

		if (tempoVolta < 3) {
			Time.timeScale = 0.1f;
			tempo.SetActive (true);
			tempo.GetComponent<Text>().text = "" + tempoVolta.ToString ("f0");
			;
		}

		if (tempoVolta >= 3) {
			Time.timeScale = 1;
			tempo.SetActive (false);
			despausou = false;
		}
	}

	public void Back ()
	{
		paused = false;
		despausou = true;
	}

	public void Restart ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
