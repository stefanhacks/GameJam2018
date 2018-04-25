using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
	private GameManager gM;

	private void Start ()
	{
		gM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	public void highFiveEnd ()
	{
		gM.proceedGameWin ();
	}
}
