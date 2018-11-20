using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Settings() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Help() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Play() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
