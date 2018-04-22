using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void NewGameBtn (string NewGameLevel) {
		SceneManager.LoadScene (NewGameLevel);
	}

	public void ExitGameBtn () {
		Application.Quit ();
	}
}
