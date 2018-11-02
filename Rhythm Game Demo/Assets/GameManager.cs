using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    int basicScore = 100;
    int multiplier = 1;
    int streak = 0;//combo

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("HP", 25);
        PlayerPrefs.SetInt("Streak", 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(collider.gameObject);
        Debug.Log("missed");//currently not working
        ResetStreak();
    }

    public void AddStreak() {
        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") + 1 <=100? PlayerPrefs.GetInt("HP") + 1:100);
        streak = PlayerPrefs.GetInt("Streak") + 1;
        PlayerPrefs.SetInt("Streak", streak);
        multiplier = (streak / 10) + 1;
        PlayerPrefs.SetInt("Multiplier", multiplier);
        //UpdateGUI();
    }

    public void ResetStreak() {
        //Debug.Log("reset");
        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") - 2 >=0? PlayerPrefs.GetInt("HP") - 2:0);
        PlayerPrefs.SetInt("Streak", 0);
        multiplier = 1;
        PlayerPrefs.SetInt("Multiplier", multiplier);
        //UpdateGUI();
    }

    //void UpdateGUI() {
    //    PlayerPrefs.SetInt("Streak", streak);
    //    PlayerPrefs.SetInt("Multiplier", multiplier);
    //}

    public int GetScore() {
        return basicScore * multiplier;
    }

    public void GameEnd() {//successfully ended a song

    }

    public void Failed() {//failed to complete a song

    }
}
