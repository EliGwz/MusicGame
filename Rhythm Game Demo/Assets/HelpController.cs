using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour {


    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Back() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void Next() {
        canvas2.gameObject.SetActive(true);
        canvas1.gameObject.SetActive(false);
    }
    public void Prev() {
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }
    public void ZoomIn() {
        canvas3.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }
    public void ZoomOut() {
        canvas2.gameObject.SetActive(true);
        canvas3.gameObject.SetActive(false);
    }
}
