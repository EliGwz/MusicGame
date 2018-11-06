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
        PlayerPrefs.SetInt("Multiplier", multiplier);
        PlayerPrefs.SetInt("Streak", 0);
        PlayerPrefs.SetInt("Perfect", 0);
        PlayerPrefs.SetInt("Great", 0);
        PlayerPrefs.SetInt("Good", 0);
        PlayerPrefs.SetInt("Miss", 0);
        SonicBloom.Koreo.Koreographer.Instance.EventDelayInSeconds=0.3f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(collider.gameObject);
        Debug.Log("missed");
        AddStat(0);
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

    public void AddStat(int stat) {//add statistics for a play. 0-3 means Miss, Good, Great and Perfect respectively.
        string[] stats = { "Miss", "Good", "Great", "Perfect" };
        int temp = PlayerPrefs.GetInt(stats[stat]) + 1;
        PlayerPrefs.SetInt(stats[stat], temp);
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

    public int GetScore(float hitPosition) {
        int accuracy;
        switch ((int)(hitPosition/0.3)) {
            case 0:
                accuracy = 10;
                break;
            case 1:
                accuracy = 7;
                break;
            default:
                accuracy = 4;
                break;
        }
        return basicScore * multiplier * accuracy / 10;
    }

    public void GameEnd() {//successfully ended a song

    }

    public void Failed() {//failed to complete a song

    }


}
