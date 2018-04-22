using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public AudioSource SomClick;

	public void NewGameBtn (string NewGameLevel) {
		SceneManager.LoadScene (NewGameLevel);

		SomClick.Play ();
		SceneManager.LoadScene (NewGameLevel);

	}

	public void ExitGameBtn () {
		Application.Quit ();
	}
}
