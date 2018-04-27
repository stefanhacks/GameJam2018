using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private GameObject gM;

	void Start () {
		//Finding References
		gM = GameObject.Find ("GameManager");
	}	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            gM.GetComponent<GameManager>().buttonPressed(KeyCode.Space);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gM.GetComponent<GameManager>().buttonPressed(KeyCode.Escape);
        }

    }

}
