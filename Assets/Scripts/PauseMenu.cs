using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	private GameObject menu, tempo;
	private bool paused = false, despausou = false;
	private float tempoVolta = 1;
	private GameManager gM;

	void Start ()
	{
		menu = GameObject.Find ("Canvas/PauseMenuManager/MenuPanel");
		tempo = GameObject.Find ("Canvas/PauseMenuManager/TempoVolta");
		gM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		menu.SetActive (false);
		tempo.SetActive (false);
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
