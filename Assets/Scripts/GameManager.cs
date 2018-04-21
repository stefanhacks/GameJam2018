using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject plataforma;

	public float tempo;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CriarPlataforma", tempo, tempo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CriarPlataforma(){
		Instantiate (plataforma, new Vector3 (0, -6, 0), transform.rotation);
	}

}
