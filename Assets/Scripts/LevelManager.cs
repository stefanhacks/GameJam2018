using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject plataforma;
	public Transform canvas;
	public float posicaox;

	private Vector3 distancia;

	private GameSettings gS;
	// Use this for initialization
	void Start () {
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings>();
		InvokeRepeating ("CriarPlataforma", gS.tempoSpawnPlataforma, gS.tempoSpawnPlataforma);
		posicaox = Random.Range(-2, -6);
		distancia.y = canvas.transform.position.y - gS.distanciaSpawnPlataforma;
	}

	// Update is called once per frame
	void Update () {
		gS.tempoSpawnPlataformaInv += Time.deltaTime;
	}

	void CriarPlataforma(){
		
		posicaox = Random.Range(-2, -6);
		gS.distanciaSpawnPlataforma = Random.Range (gS.randomTempoSpawnPlatMin, gS.randomTempoSpawnPlatMax);
		GameObject plataforma1 = Instantiate (plataforma, new Vector3 (posicaox, distancia.y - gS.distanciaSpawnPlataforma, 0), Quaternion.identity) as GameObject;

		distancia = plataforma1.transform.position;

		Vector3 posicao2y = new Vector3 (posicaox * -1, plataforma1.transform.position.y, 0);
		GameObject plataforma2 = Instantiate (plataforma, posicao2y, Quaternion.identity) as GameObject;
		//go.transform.parent = GameObject.Find ("Canvas").transform; 
		//go.transform.localScale = new Vector2 (3000, 350);

		if (gS.tempoSpawnPlataformaInv >= 6) {
			float randominv = Random.Range (0, 5);
		
			if (randominv == 1) {
				
				int escolha = Random.Range (0, 2);

				if (escolha == 1) {
					plataforma1.GetComponent<SpriteRenderer> ().enabled = false;
				} else {
					plataforma2.GetComponent<SpriteRenderer> ().enabled = false;
				}
				gS.tempoSpawnPlataformaInv = 0f;
			}
		}
	}
}
