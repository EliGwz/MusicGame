using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectionController : MonoBehaviour {


    public RawImage songImage;
    public Text difficulty;
    public Image difficultyButton;
    public Text length;
    public Text bpm;
    public Text songName;
    public Text noteSpeed;
    public static int songIndex;
    public AudioSource songDemo;
    float speed;
    Color easyColor = new Color(0.3f, 1f, 0f);
    Color hardColor = new Color(1f, 0.5f, 0.3f);

    // Use this for initialization
    void Start () {
        //GameStatics.Song bladeDance = new GameStatics.Song("Blade Dance", "187", "01:30", "Resources/SongImages/BladeDance.png");
        songIndex = PlayerPrefs.GetInt("SongIndex", 0);
        difficulty.text = PlayerPrefs.GetString("Difficulty", "Easy");
        speed = PlayerPrefs.GetFloat("noteSpeed", 6f);
        noteSpeed.text = speed.ToString("F1") + "";

        PlayerPrefs.SetString("Difficulty", difficulty.text);
        if (difficulty.text == "Easy") {
            difficultyButton.color = easyColor;
        } else {
            difficultyButton.color = hardColor;
        }
        LoadSongInfo(songIndex);
        
    }
	

    public void ChangeDifficulty() {
        if (difficulty.text == "Easy") {
            difficulty.text = "Hard";
            difficultyButton.color = hardColor;
        } else {
            difficulty.text = "Easy";
            difficultyButton.color = easyColor;
        }
        PlayerPrefs.SetString("Difficulty", difficulty.text);
    }

    public void MoveLeft() {
        songIndex -= 1;
        if (songIndex < 0) {
            songIndex = GameStatics.songs.Length - 1;
        }
        LoadSongInfo(songIndex);  
    }

    public void MoveRight() {
        songIndex += 1;
        if (songIndex > GameStatics.songs.Length - 1) {
            songIndex = 0;
        }
        LoadSongInfo(songIndex);
    }

    public void IncreaseNoteSpeed() {
        speed += 0.5f;
        if (speed > 10) {
            speed = 10f;
        }
        PlayerPrefs.SetFloat("noteSpeed", speed);
        noteSpeed.text = speed.ToString("F1") + "";
    }

    public void DecreaseNoteSpeed() {
        speed -= 0.5f;
        if (speed < 1) {
            speed = 1f;
        }
        PlayerPrefs.SetFloat("noteSpeed", speed);
        noteSpeed.text = speed.ToString("F1") + "";
    }

    void LoadSongInfo(int songIndex) {
        bpm.text = GameStatics.songs[songIndex].Bpm;
        length.text = GameStatics.songs[songIndex].Length;
        songName.text = GameStatics.songs[songIndex].Name;
        songImage.texture = Resources.Load(GameStatics.songs[songIndex].ImagePath) as Texture2D;
        PlayerPrefs.SetString("SongName", GameStatics.songs[songIndex].PlayBackName);//should made koreography's name integrated with playBackName
        songDemo.Stop();
        songDemo.clip = Resources.Load(GameStatics.songs[songIndex].MusicDemoPath) as AudioClip;
        songDemo.Play();
    }

    public void Play() {
        PlayerPrefs.SetString("PlayMode", "Play");
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void Back() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
