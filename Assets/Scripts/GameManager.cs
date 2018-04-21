using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private GameObject[] player;

	public GameObject textoSlow;

	public float tempoSlow = 0;

	public Text textoTempoSlow;

	public bool comecou = false;
	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player");
    }
	

	void Update () {
      //  Morrer(player[0]);
       // Morrer(player[1]);

		if (!comecou) {
			Slow ();
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
			comecou = true;
		}
	}

    void Morrer (GameObject play)
    {
		if (Blitzkrieg.GetGameObjectPosition(play).y <= -0.025 || Blitzkrieg.GetGameObjectPosition(play).y > 1.025) {
            Destroy(play);
        }
    }

}
