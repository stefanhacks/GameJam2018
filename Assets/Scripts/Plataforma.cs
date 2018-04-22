using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

	Vector3 posicaoInicial;
	private GameSettings gS;
	private SpriteRenderer sR;
	// Use this for initialization
	void Start () {
		posicaoInicial = this.transform.position;
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		sR.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (posicaoInicial.x + Mathf.Sin (Time.time) * 2, posicaoInicial.y, posicaoInicial.z);

		if (gS.tempoSpawnPlataformaInv >= 6) {
			float randominv = Random.Range (0, 5);

			if (randominv == 1) {

				int escolha = Random.Range (0, 2);

				if (escolha == 1) {
					sR.enabled = false;
				} else {
					sR.enabled = false;
				}
				gS.tempoSpawnPlataformaInv = 0f;
			}
		}

		if (gS.tempoSpawnPlataformaMove >= 6) {
			float randomMove = Random.Range (0, 5);

			if (randomMove == 1) {

				int escolha = Random.Range (0, 2);

				if (escolha == 1) {
				//	if(transform.position.x

					//	}
						gS.tempoSpawnPlataformaMove = 0f;
						}
			}

	}
}
}
