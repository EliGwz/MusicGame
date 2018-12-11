using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    int basicScore = 100;
    int multiplier = 1;
    int streak = 0;//combo
    int maxCombo = 0;
    string device;
    public static float volume;
    public float volume0;
    public static float volumeThreshold;//the minimum volume to hit voice notes
    AudioClip micRecord;
    public Text hitText;
    //public TextMeshProUGUI hitTextMesh;

    public Camera camera;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("HP", 25);
        PlayerPrefs.SetInt("Multiplier", multiplier);
        PlayerPrefs.SetInt("Streak", 0);
        PlayerPrefs.SetInt("Perfect", 0);
        PlayerPrefs.SetInt("Great", 0);
        PlayerPrefs.SetInt("Good", 0);
        PlayerPrefs.SetInt("Bad", 0);
        PlayerPrefs.SetInt("Miss", 0);
        PlayerPrefs.SetInt("MaxCombo", 0);
        PlayerPrefs.SetFloat("Volume", 0f);
        SonicBloom.Koreo.Koreographer.Instance.EventDelayInSeconds=PlayerPrefs.GetFloat("NoteDisplayOffset",0.3f);
        volumeThreshold = PlayerPrefs.GetFloat("VolumeThreshold", 0.3f);
        device = Microphone.devices[0];
        micRecord = Microphone.Start(device, true, 300, 44100);

    }
	
	// Update is called once per frame
	void Update () {
        volume = GetVolume();
        volume0 = volume;
        PlayerPrefs.SetFloat("Volume", volume*1);
    }

    float GetVolume() {
        float maxVolume = 0f;

        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(device) - 127;
        if (offset < 0) {
            return 0;
        }
        micRecord.GetData(volumeData, offset);
        for(int i = 0; i < volumeData.Length; i++) {
            if (maxVolume < volumeData[i]) {
                maxVolume = volumeData[i];
            }
        }
        return maxVolume;

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
        if (streak > maxCombo) {
            maxCombo = streak;
        }
        multiplier = (streak / 10) + 1;
        PlayerPrefs.SetInt("Multiplier", multiplier);
        UpdateGUI();
    }

    public void AddStat(int stat) {//add statistics for a play. 0-4 means Miss, Bad, Good, Great and Perfect respectively.
        string[] stats = { "Miss", "Bad", "Good", "Great", "Perfect" };
        int temp = PlayerPrefs.GetInt(stats[stat]) + 1;
        PlayerPrefs.SetInt(stats[stat], temp);
        hitText.text = stats[stat];
    }

    public void ResetStreak() {
        //Debug.Log("reset");
        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") - 2 >=0? PlayerPrefs.GetInt("HP") - 2:0);
        PlayerPrefs.SetInt("Streak", 0);
        multiplier = 1;
        PlayerPrefs.SetInt("Multiplier", multiplier);
        UpdateGUI();
    }

    void UpdateGUI() {
        camera.backgroundColor = new Color(streak / 255f, streak / 255f, streak / 255f, 0);
    }

    public int GetScore(float hitPosition) {
        int accuracy;
        switch ((int)(hitPosition/0.3/PlayerPrefs.GetFloat("noteSpeed")*6)) {
            case 0:
                accuracy = 10;
                break;
            case 1:
                accuracy = 7;
                break;
            case 2:
            case 3:
                accuracy = 4;
                break;
            default:
                accuracy = 1;
                ResetStreak();
                break;
        }
        return basicScore * multiplier * accuracy / 10;
    }

    public void GameEnd() {//successfully ended a song
        Debug.Log("end");
        PlayerPrefs.SetInt("MaxCombo", maxCombo);
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void Failed() {//failed to complete a song

    }


}
