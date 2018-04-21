using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private GameObject[] player;

    public GameObject textoSlow, painelGameOver;

	public float tempoSlow = 0;

	public Text textoTempoSlow;

    public enum GameState { Readying, Playing, GameOver }
    public GameState currentState = GameState.Playing;

	public bool comecou = false;
	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player");
        //painelGameOver = GameObject.FindGameObjectWithTag("GameOverPainel"); // Não acha a referencia, por quê?
    }
	

	void Update () {
        if (currentState == GameState.Readying)
        {
            Slow();
        } else if (currentState == GameState.Playing)
        {
            checkDeath(player[0]);
            checkDeath(player[1]);
        } else if (currentState == GameState.GameOver)
        {

        }
    }

	void Slow() {
		tempoSlow -= Time.deltaTime *10;

		if (tempoSlow > 1) {
			Time.timeScale = 0.1f;
			textoSlow.SetActive(true);
			textoTempoSlow.text = "" + tempoSlow.ToString("f0"); ;
		}

		if (tempoSlow <= 0 && tempoSlow >= -1) {
			textoTempoSlow.text = "GO"; ;
		}

		if (tempoSlow < -1)
		{
			Time.timeScale = 1;
			textoSlow.SetActive(false);
            currentState = GameState.Playing;
            player[0].GetComponent<PlayerController>().movementEnabled = true;
            player[1].GetComponent<PlayerController>().movementEnabled = true;
        }
	}

    void checkDeath (GameObject play)
    {
		if (Blitzkrieg.GetGameObjectPosition(play).y <= -0.025 || Blitzkrieg.GetGameObjectPosition(play).y > 1.025) {
            painelGameOver.SetActive(true);
            currentState = GameState.GameOver;
            //Destroy(play);
        }
    }


    public void buttonPressed(KeyCode keyPressed)
    {
        if (keyPressed == KeyCode.Space && currentState == GameState.GameOver)
        {
            SceneManager.LoadScene(1);
        } else if (keyPressed == KeyCode.Escape && currentState == GameState.GameOver)
        {
            SceneManager.LoadScene(0);
        }
    }
}
