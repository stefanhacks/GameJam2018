using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	private GameObject plataforma, chave;
	private Transform canvas;
	public float posicaox, tempoSpawnPlataforma, tempoSpawnChave;
	private Vector3 distancia;
	private GameSettings gS;
	private GameManager gM;
	private Sprite plataforma2Sprite;

	void Start ()
	{
		//Loading Resources
		plataforma2Sprite = Resources.Load <Sprite> ("Graphics/Images/PlatPink");
		plataforma = Resources.Load ("Prefabs/Plataforma") as GameObject;
		chave = Resources.Load ("Prefabs/Key") as GameObject;

		//Finding References
		gS = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		gM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		canvas = GameObject.Find ("Canvas").GetComponent<Transform> ();  

		distancia.y = canvas.transform.position.y - gS.distanciaSpawnPlataforma;
		InvokeRepeating ("CriarPlataforma", gS.tempoSpawnPlataforma - 1, gS.tempoSpawnPlataforma);
		posicaox = Random.Range (-2, -7);
		tempoSpawnChave = Random.Range (gS.tempoSpawnChaveMin, gS.tempoSpawnChaveMax);
	}

	void Update ()
	{
		tempoSpawnChave -= Time.deltaTime;
	}

	void CriarPlataforma ()
	{
		if (!gM.venceu) {
			posicaox = Random.Range (-2, -7);
			tempoSpawnPlataforma = Random.Range (gS.randomTempoSpawnPlatMin, gS.randomTempoSpawnPlatMax);
			GameObject plataforma1 = Instantiate (plataforma, new Vector3 (posicaox, distancia.y - gS.distanciaSpawnPlataforma, 0), Quaternion.identity) as GameObject;
			distancia = plataforma1.transform.position;

			Vector3 posicao2y = new Vector3 (posicaox * -1, plataforma1.transform.position.y, 0);
			GameObject plataforma2 = Instantiate (plataforma, posicao2y, Quaternion.identity) as GameObject;
			plataforma2.GetComponent<SpriteRenderer> ().sprite = plataforma2Sprite;

			int chance = Random.Range (0, 5);
			if (chance == 0) {
				if (Random.Range (0, 2) == 0) {
					plataforma1.GetComponent<Plataforma> ().setMovel ();
				} else {
					plataforma2.GetComponent<Plataforma> ().setMovel ();
				}
			} else if (chance == 1) {
				if (Random.Range (0, 2) == 0) {
					plataforma1.GetComponent<Plataforma> ().setInv ();
				} else {
					plataforma2.GetComponent<Plataforma> ().setInv ();
				}
			}

			if (tempoSpawnChave <= 0) {
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
						key2.GetComponent<SpriteRenderer> ().enabled = false;
					} else {
						float randomx = Random.Range (-3, 3);
						float randomy = Random.Range (-1.5f, -3);
						GameObject key1 = Instantiate (chave, new Vector3 (plataforma2.transform.position.x + randomx, plataforma2.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;
						randomx *= -1;
						GameObject key2 = Instantiate (chave, new Vector3 (plataforma1.transform.position.x + randomx, plataforma1.transform.position.y + randomy, 0), Quaternion.identity) as GameObject;

						key1.GetComponent<BoxCollider2D> ().enabled = false;
						key2.GetComponent<SpriteRenderer> ().enabled = false;
					}
				}
				tempoSpawnChave = Random.Range (gS.tempoSpawnChaveMin, gS.tempoSpawnChaveMax);
			}			 
		}
	}
}
