using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject[] player;

	void Start () {

        player = GameObject.FindGameObjectsWithTag("Player");

    }
	

	void Update () {
        Morrer(player[0]);
        Morrer(player[1]);
    }

    void Morrer (GameObject play)
    {
		if (Blitzkrieg.GetGameObjectPosition(play).y <= -0.025 || Blitzkrieg.GetGameObjectPosition(play).y > 1.025) {
            Destroy(play);
        }
    }

}
