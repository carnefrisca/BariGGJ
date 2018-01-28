using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_script : MonoBehaviour {



	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Start")) 
		{
			GameStart ();
			Debug.Log ("Start");
		}
		if (Input.GetButtonDown ("escape")) 
		{
			QuitApplication ();
			Debug.Log ("Esci");
		}
	}
		

	void GameStart ()
	{

		GetComponent<CanvasGroup>().alpha = 0;
		Time.timeScale = 1;
	}



	void QuitApplication()
	{
		Application.Quit();
	}

}
