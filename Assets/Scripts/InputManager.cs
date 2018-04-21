using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject gm;

	void Start () {
		
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            gm.GetComponent<GameManager>().buttonPressed(KeyCode.Space);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.GetComponent<GameManager>().buttonPressed(KeyCode.Escape);
        }

    }

}
