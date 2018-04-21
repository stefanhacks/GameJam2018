using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject player1, player2;

	void Start () {

        player1 = GameObject.FindGameObjectWithTag("Player1");
        //player2 = GameObject.FindGameObjectWithTag("Player2");

    }
	

	void Update () {
        Morrer(player1);
        //Morrer(player2);
    }

    void Morrer (GameObject play)
    {
        if (Blitzkrieg.GetGameObjectPosition(player1).y <= -0.3 || Blitzkrieg.GetGameObjectPosition(player1).y > 1.3) {
            Destroy(play);
        }
    }

}
