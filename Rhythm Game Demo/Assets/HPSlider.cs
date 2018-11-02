using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSlider : MonoBehaviour {


    GameObject needle;
    float hp;
    

	// Use this for initialization
	void Start () {
        needle = transform.Find("needle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        hp = PlayerPrefs.GetInt("HP");
        needle.transform.localPosition = new Vector3(hp/10f, 0, 0);
	}
}
