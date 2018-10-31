using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void empezar(){

	//	SceneManager.LoadScene("StarterScene"); 
	//	Debug.Log(SceneManager.GetSceneByName("StarterScene"));
	SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1); 

	}

	public void salir(){

	//	SceneManager.LoadScene("StarterScene"); 
	Application.Quit();
	
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
