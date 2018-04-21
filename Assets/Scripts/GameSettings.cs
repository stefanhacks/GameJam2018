using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

	public float velocidadeCamera, tempoSpawnPlataforma, tempoSpawnPlataformaInv, velocidadePlayers, tempoJogo, alturaPlayers;

	public Text textoTempo, textoMetros;

	public GameObject[] player;

	public Vector3 posicaoPlayerInicial;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player");	
		posicaoPlayerInicial.y = player[0].transform.position.y;


	}
	
	// Update is called once per frame
	void Update () {
		tempoJogo += Time.deltaTime;
		textoTempo.text = (tempoJogo.ToString ("f0"));
		for(int i = 0; i < player.Length; i++)
		{
			alturaPlayers = posicaoPlayerInicial.y + player[i].transform.position.y *-1;
		}

		textoMetros.text = (alturaPlayers.ToString ("0" + " m"));

	}
}
