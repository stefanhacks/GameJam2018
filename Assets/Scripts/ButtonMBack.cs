using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonMBack : MonoBehaviour {

	public AudioSource SomBack;

	public void NewGameBtn (string NewGameLevel) {
		SceneManager.LoadScene (NewGameLevel);

		SomBack.Play ();
		SceneManager.LoadScene (NewGameLevel);
	}

	public void ExitGameBtn () {
		Application.Quit ();
	}
}
