using System.Collections;
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

    // Use this for initialization
    void Start () {
        int songIndex = PlayerPrefs.GetInt("SongIndex", 0);
        songImage.texture = Resources.Load(GameStatics.songs[songIndex].ImagePath) as Texture2D;
        songName.text = GameStatics.songs[songIndex].Name + " [" + PlayerPrefs.GetString("Difficulty", "Easy") + "]";
        perfect.text = PlayerPrefs.GetInt("Perfect") + "";
        great.text = PlayerPrefs.GetInt("Great") + "";
        good.text = PlayerPrefs.GetInt("Good") + "";
        bad.text = PlayerPrefs.GetInt("Bad") + "";
        miss.text = PlayerPrefs.GetInt("Miss") + "";
        maxCombo.text = PlayerPrefs.GetInt("MaxCombo") + "";
        score.text = PlayerPrefs.GetInt("Score") + "";
        float techScore = PlayerPrefs.GetInt("Perfect") + PlayerPrefs.GetInt("Great") * 0.7f + PlayerPrefs.GetInt("Good") * 0.4f + PlayerPrefs.GetInt("Bad") * 0.1f;
        float fullScore = PlayerPrefs.GetInt("Perfect") + PlayerPrefs.GetInt("Great") + PlayerPrefs.GetInt("Good") + PlayerPrefs.GetInt("Bad") + PlayerPrefs.GetInt("Miss");
        if (techScore / fullScore >= 0.97) {//S
            scoreRanking.texture = Resources.Load(GameStatics.rankSPath) as Texture2D;
            cheersSound.PlayOneShot(Resources.Load(GameStatics.cheersPath) as AudioClip);
        } else if (techScore / fullScore >= 0.92) {//A
            scoreRanking.texture = Resources.Load(GameStatics.rankAPath) as Texture2D;
            cheersSound.PlayOneShot(Resources.Load(GameStatics.cheersPath) as AudioClip);
        } else if (techScore / fullScore >= 0.85) {//B
            scoreRanking.texture = Resources.Load(GameStatics.rankBPath) as Texture2D;
        } else if (techScore / fullScore >= 0.7) {//C
            scoreRanking.texture = Resources.Load(GameStatics.rankCPath) as Texture2D;
        } else {//D
            scoreRanking.texture = Resources.Load(GameStatics.rankDPath) as Texture2D;
        }
        scoreSound.Play();
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
