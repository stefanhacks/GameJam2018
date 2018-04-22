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
			this.transform.position = posicaoInicial;
		}
	}

	public void setInv () {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void setMovel () {
		if (Blitzkrieg.GetGameObjectXFromCenter (this.gameObject) > 0) {
			this.transform.position =
				new Vector3 (
					Camera.main.ViewportToWorldPoint (new Vector3 (0.75f, 0, 0)).x,
					this.transform.position.y,
					this.transform.position.z
				);
		} else {
			this.transform.position =
				new Vector3 (
					Camera.main.ViewportToWorldPoint (new Vector3 (0.25f, 0, 0)).x,
					this.transform.position.y,
					this.transform.position.z
				);
		}
		this.movel = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Paredes") {
			this.posicaoAtual = this.transform.position;
			parede = !parede;
		}
	}
}
