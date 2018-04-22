using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

	public float velocidadeCamera, tempoSpawnPlataforma, tempoSpawnPlataformaInv, velocidadePlayers, tempoJogo, 
	alturaPlayers, velocidadeParedeMeio, distanciaSpawnPlataforma, randomTempoSpawnPlatMin, randomTempoSpawnPlatMax, tempoSpawnChave;

	public Text textoTempo, textoMetros;
	public GameObject[] player;
	public Vector3 posicaoPlayerInicial;
	public bool moveCamera = true;

	public int quantidadeChave;
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player");	
		posicaoPlayerInicial.y = player[0].transform.position.y;
	
	}

	void Update () {
		textoTempo.text = (tempoJogo.ToString ("0" + " s"));

		for (int i = 0; i < player.Length; i++) {
			alturaPlayers = posicaoPlayerInicial.y + player[i].transform.position.y * -1;
		}
		textoMetros.text = (alturaPlayers.ToString ("0" + " m"));

	}
}

