using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;

	public Slider volumeSlider;
	private AudioSource[] volumeAudio;

	public static AudioManager Instance {
		get { return instance; }
	}

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	void Start(){

	
	}

	void Update(){
		if (volumeSlider == null) {
			Scene currentScene = SceneManager.GetActiveScene ();
				
			string sceneName = currentScene.name;
			Debug.Log ("cena " + sceneName); 

		if (sceneName == "Main") {
				volumeSlider = GameObject.Find ("Canvas/PauseMenuManager/MenuPanel/Slider").GetComponent<Slider> ();
				volumeAudio = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
			}			
		}
	}

	public void VolumeController(){
		foreach(AudioSource volumeAudio in volumeAudio){
			volumeAudio.volume = volumeSlider.value;
		}
	}

}
