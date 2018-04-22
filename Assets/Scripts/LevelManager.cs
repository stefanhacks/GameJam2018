using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

	public GameObject plataforma, chave;
	public Transform canvas;
	public float posicaox;

	private Vector3 distancia;

	private GameSettings gS;

	public Sprite plataforma2Sprite;
	// Use this for initialization
	void Start () {
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings>();
		InvokeRepeating ("CriarPlataforma", gS.tempoSpawnPlataforma, gS.tempoSpawnPlataforma);
		posicaox = Random.Range(-2, -6);
		distancia.y = canvas.transform.position.y - gS.distanciaSpawnPlataforma;
		gS.tempoSpawnChave = Random.Range (-2, -5);
	}

	// Update is called once per frame
	void Update () {
		gS.tempoSpawnPlataformaInv += Time.deltaTime;
		gS.tempoSpawnChave += Time.deltaTime;
		gS.tempoSpawnPlataformaMove += Time.deltaTime;
	}

	void CriarPlataforma(){
		if (!gS.venceu) {
			posicaox = Random.Range (-2, -6);
			gS.distanciaSpawnPlataforma = Random.Range (gS.randomTempoSpawnPlatMin, gS.randomTempoSpawnPlatMax);
			GameObject plataforma1 = Instantiate (plataforma, new Vector3 (posicaox, distancia.y - gS.distanciaSpawnPlataforma, 0), Quaternion.identity) as GameObject;
			distancia = plataforma1.transform.position;

			Vector3 posicao2y = new Vector3 (posicaox * -1, plataforma1.transform.position.y, 0);
			GameObject plataforma2 = Instantiate (plataforma, posicao2y, Quaternion.identity) as GameObject;
			plataforma2.GetComponent<SpriteRenderer> ().sprite = plataforma2Sprite;
			//go.transform.parent = GameObject.Find ("Canvas").transform; 
			//go.transform.localScale = new Vector2 (3000, 350);

			if (gS.tempoSpawnChave >= 0) {
				int escolha = Random.Range (0, 2);
				if (escolha == 1) {
					int escolhaY = Random.Range (0, 2);
					if (escolhaY == 1) {
						float randomx = Random.Range (-3, 3);
						float randomy = Random.Range (0.7f, 3);
						GameObject key1 = Instantiate (chave, new Vector3 (plataforma1.transform.position.x + randomx, plataforma1.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
						randomx *= -1;
						GameObject key2 = Instantiate (chave, new Vector3 (plataforma2.transform.position.x + randomx, plataforma2.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
		
						key1.GetComponent<BoxCollider2D> ().enabled = false;
						key2.GetComponent<SpriteRenderer> ().enabled = false;
					} else {
						float randomx = Random.Range (-3, 3);
						float randomy = Random.Range (-1.5f, -3);
						GameObject key1 = Instantiate (chave, new Vector3 (plataforma1.transform.position.x + randomx, plataforma1.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
						randomx *= -1;
						GameObject key2 = Instantiate (chave, new Vector3 (plataforma2.transform.position.x + randomx, plataforma2.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;

						key1.GetComponent<BoxCollider2D> ().enabled = false;
						key2.GetComponent<SpriteRenderer> ().enabled = false;
					}
				} else {

					int escolhaY = Random.Range (0, 2);
					if (escolhaY == 1) {
						float randomx = Random.Range (-3, 3);
						float randomy = Random.Range (0.7f, 3);
						GameObject key1 = Instantiate (chave, new Vector3 (plataforma2.transform.position.x + randomx, plataforma2.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
						randomx *= -1;
						GameObject key2 = Instantiate (chave, new Vector3 (plataforma1.transform.position.x + randomx, plataforma1.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;

						key1.GetComponent<BoxCollider2D> ().enabled = false;
						//key2.GetComponent<SpriteRenderer> ().enabled = false;
					} else {
						float randomx = Random.Range (-3, 3);
						float randomy = Random.Range (-1.5f, -3);
						GameObject key1 = Instantiate (chave, new Vector3 (plataforma2.transform.position.x + randomx, plataforma2.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
						randomx *= -1;
						GameObject key2 = Instantiate (chave, new Vector3 (plataforma1.transform.position.x + randomx, plataforma1.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;

						key1.GetComponent<BoxCollider2D> ().enabled = false;
						//key2.GetComponent<SpriteRenderer> ().enabled = false;
					}
				}
				gS.tempoSpawnChave = Random.Range (-2, -5);
			}			 
		}
	}
}
