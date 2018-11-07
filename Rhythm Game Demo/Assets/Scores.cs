using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public string textName;

	// Update is called once per frame
	void Update () {
        if (textName != "Volume") {
            GetComponent<Text>().text = PlayerPrefs.GetInt(textName) + "";
        } else {
            GetComponent<Text>().text = PlayerPrefs.GetFloat (textName) + "";
        }

        
	}
}
