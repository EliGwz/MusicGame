﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSceneController : MonoBehaviour {

    public Text perfect;
    public Text great;
    public Text good;
    public Text bad;
    public Text miss;
    public Text maxCombo;
    public Text score;
    public RawImage songImage;
    public Text songName;
    public RawImage scoreRanking;
    public AudioSource scoreSound;
    public AudioSource cheersSound;
    private string Url = "https://i.cs.hku.hk/~wzgao/Upload_score.php";

    // Use this for initialization
    void Start () {
        int songIndex = PlayerPrefs.GetInt("SongIndex", 0);
        if (PlayerPrefs.GetString("PlayMode", "Play") == "Test") {
            songImage.texture = Resources.Load(GameStatics.testSong.ImagePath) as Texture2D;
            songName.text = GameStatics.testSong.Name + " [" + PlayerPrefs.GetString("Difficulty", "Easy") + "]";
        } else {
            songImage.texture = Resources.Load(GameStatics.songs[songIndex].ImagePath) as Texture2D;
            songName.text = GameStatics.songs[songIndex].Name + " [" + PlayerPrefs.GetString("Difficulty", "Easy") + "]";
        }
        perfect.text = PlayerPrefs.GetInt("Perfect") + "";
        great.text = PlayerPrefs.GetInt("Great") + "";
        good.text = PlayerPrefs.GetInt("Good") + "";
        bad.text = PlayerPrefs.GetInt("Bad") + "";
        miss.text = PlayerPrefs.GetInt("Miss") + "";
        maxCombo.text = PlayerPrefs.GetInt("MaxCombo") + "";
        score.text = PlayerPrefs.GetInt("Score") + "";
        float techScore = PlayerPrefs.GetInt("Perfect") + PlayerPrefs.GetInt("Great") * 0.7f + PlayerPrefs.GetInt("Good") * 0.4f + PlayerPrefs.GetInt("Bad") * 0.1f;
        float fullScore = PlayerPrefs.GetInt("Perfect") + PlayerPrefs.GetInt("Great") + PlayerPrefs.GetInt("Good") + PlayerPrefs.GetInt("Bad") + PlayerPrefs.GetInt("Miss");
        if (techScore / fullScore >= 0.95) {//S
            scoreRanking.texture = Resources.Load(GameStatics.rankSPath) as Texture2D;
            cheersSound.PlayOneShot(Resources.Load(GameStatics.cheersPath) as AudioClip);
        } else if (techScore / fullScore >= 0.9) {//A
            scoreRanking.texture = Resources.Load(GameStatics.rankAPath) as Texture2D;
            cheersSound.PlayOneShot(Resources.Load(GameStatics.cheersPath) as AudioClip);
        } else if (techScore / fullScore >= 0.8) {//B
            scoreRanking.texture = Resources.Load(GameStatics.rankBPath) as Texture2D;
        } else if (techScore / fullScore >= 0.7) {//C
            scoreRanking.texture = Resources.Load(GameStatics.rankCPath) as Texture2D;
        } else {//D
            scoreRanking.texture = Resources.Load(GameStatics.rankDPath) as Texture2D;
        }
        scoreSound.Play();


        //Upload Score
        if (PlayerPrefs.GetInt("LogStat") == 1 && PlayerPrefs.GetInt("Score") != 0)
        {
            if(PlayerPrefs.GetString("PlayMode", "Play") == "Play") {
                CreateMainForm(PlayerPrefs.GetString("UserID"), PlayerPrefs.GetInt("SongIndex"),
                PlayerPrefs.GetInt("Score"), PlayerPrefs.GetString("Difficulty"));
            }
        }
        else
        {
            Debug.Log("not login");
        }
        
    }

    public void CreateMainForm(string usrId, int songId, int score, string diff)
    {
        WWWForm form = new WWWForm();
        form.AddField("usr_ID", usrId);
        form.AddField("song_ID", songId);
        form.AddField("score", score);
        form.AddField("song_diff", diff);
        StartCoroutine(SendPost(Url, form));
    }

    IEnumerator SendPost(string url, WWWForm form)
    {
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }

    public void Retry() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void Next() {
        if (PlayerPrefs.GetString("PlayMode", "Play") == "Play") {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);//back to songlist
        } else {
            PlayerPrefs.SetString("PlayMode", "Play");
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);//back to settings
        }
    }
}
