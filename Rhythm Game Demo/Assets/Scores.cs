using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public string textName;

	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = PlayerPrefs.GetInt(textName) + "";
	}
}
