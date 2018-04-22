using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

	public float velocidadeCamera, tempoSpawnPlataforma, tempoSpawnPlataformaInv, velocidadePlayers, tempoJogo, 
	alturaPlayers, velocidadeParedeMeio, distanciaSpawnPlataforma, randomTempoSpawnPlatMin, randomTempoSpawnPlatMax, tempoSpawnChave, pontuacaoFinal;

	public Text textoTempo, textoMetros;
	public Vector3 posicaoPlayerInicial;
	public bool moveCamera = true;
	public bool venceu = false;
	public bool fim = false;
	public int quantidadeChave;

	void Start () {
	
	}

	void Update () {
		textoTempo.text = (tempoJogo.ToString ("0" + " s"));
		textoMetros.text = (alturaPlayers.ToString ("0" + " m"));
	}
}

