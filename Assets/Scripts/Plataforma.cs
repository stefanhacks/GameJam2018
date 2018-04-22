using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

	public Vector3 posicaoInicial, posicaoAtual;
	public bool movel = false, parede = false;

	// Use this for initialization
	void Start () {
		posicaoInicial = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (movel && !parede) {
			this.transform.position = new Vector3 (posicaoInicial.x + Mathf.Sin (Time.time) * 2, posicaoInicial.y, posicaoInicial.z);
		} else if(parede){
			this.transform.position = posicaoAtual;
		}
	}

	public void setInv () {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Paredes") {
			this.posicaoAtual = this.transform.position;
			parede = !parede;
		}
	}
}
