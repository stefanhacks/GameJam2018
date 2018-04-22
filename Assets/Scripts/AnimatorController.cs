using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
    }

    public void highFiveEnd ()
    {
        gm.proceedGameWin();
    }
}
