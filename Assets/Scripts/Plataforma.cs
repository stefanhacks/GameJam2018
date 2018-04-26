using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
	private Vector3 posicaoInicial, posicaoAtual;
	private bool movel = false;
	private GameObject target = null;
	private Vector3 offset;

	void Start ()
	{
		posicaoInicial = this.transform.position;
		target = null;
	}

	void Update ()
	{
		if (movel) {
			this.transform.position = new Vector3 (posicaoInicial.x + Mathf.Sin (Time.time) * 2, posicaoInicial.y, posicaoInicial.z);
		}
	}

	public void setInv ()
	{
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void setMovel ()
	{
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

	void OnTriggerStay2D (Collider2D col)
	{
		target = col.gameObject;
		offset = target.transform.position - transform.position;
	}

	void OnTriggerExit2D (Collider2D col)
	{
		target = null;
	}

	void LateUpdate ()
	{
		if (target != null) {
			target.transform.position = transform.position + offset;
		}
	}
}
