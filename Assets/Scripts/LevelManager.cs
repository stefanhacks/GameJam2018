using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject plataforma, player;
	public Transform canvas;
	public float tempo, tempoinv;

	public Vector3 distancia;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CriarPlataforma", tempo, tempo);
	}
	
	// Update is called once per frame
	void Update () {
		tempoinv += Time.deltaTime;
	}

	void CriarPlataforma(){
		float posicaox = Random.Range(-2, -6);
		Vector3 posicaoy = new Vector3 (posicaox,canvas.transform.position.y -10,0);
		GameObject plataforma1 = Instantiate (plataforma, posicaoy, Quaternion.identity) as GameObject;

		Vector3 posicao2y = new Vector3 (posicaox * -1, plataforma1.transform.position.y, 0);
		GameObject plataforma2 = Instantiate (plataforma, posicao2y, Quaternion.identity) as GameObject;
		//go.transform.parent = GameObject.Find ("Canvas").transform; 
		//go.transform.localScale = new Vector2 (3000, 350);

		if (tempoinv >= 10) {
			float randominv = Random.Range (0, 10);
		
			if (randominv == 1) {
				
				int escolha = Random.Range (0, 2);

				if (escolha == 1) {
					plataforma1.GetComponent<SpriteRenderer> ().enabled = false;
				} else {
					plataforma2.GetComponent<SpriteRenderer> ().enabled = false;
				}
				tempoinv = 0f;
			}
		}
	}
}
