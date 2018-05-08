using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	public AudioSource SomClick;

	public List<GameObject> botoes;
	private GameObject criado, icone, canvas;
	private Transform botao;

	private int opcoes = 0, posicao = 1;

	void Start(){
		icone = Resources.Load ("Prefabs/IconeMenu") as GameObject;
		canvas = GameObject.Find ("Canvas");
		botao = canvas.transform.Find ("Buttons");

		for (int i = 0; i < botao.childCount; i++){
			GameObject filhoBotao = botao.transform.GetChild (i).gameObject;
			botoes.Add(filhoBotao);
			opcoes++;
		}

		criado = Instantiate (icone, botoes [0].transform.position, Quaternion.identity);
	}

	void Update(){
		PosicoesMenu ();
	}

	void PosicoesMenu(){

		for (int i = 0; i < posicao; i++) {
			criado.transform.position = botoes [i].transform.position;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			if (posicao < opcoes)
			{
				posicao++;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			if (posicao > 1)
			{
				posicao--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			for (int i = 0; i < posicao; i++) {
				botao.transform.GetChild (posicao-1).GetComponent<Button> ().onClick.Invoke ();
			}
		}
	}

	public void NewGameBtn (string NewGameLevel) {
		SomClick.Play ();
		SceneManager.LoadScene (NewGameLevel);
	}

	public void ExitGameBtn () {
		Application.Quit ();
	}
}
