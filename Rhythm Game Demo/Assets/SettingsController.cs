using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public Text noteDisplayOffset;
    public Text hitOffset;
    public Text volumeThreshold;
    float noteDisplay;
    float hit;
    float volume;
    //public AudioSource bgm;

    // Use this for initialization
    void Start () {
        noteDisplay = PlayerPrefs.GetFloat("NoteDisplayOffset", 0.3f);
        hit = PlayerPrefs.GetFloat("hitDelay", 0.08f);
        volume = PlayerPrefs.GetFloat("VolumeThreshold", 0.3f);
        noteDisplayOffset.text = noteDisplay.ToString("F2") + "";
        hitOffset.text = hit.ToString("F2") + "";
        volumeThreshold.text = volume.ToString("F2") + "";
        //bgm.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Back() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Reset() {
        noteDisplay = 0.3f;
        hit = 0.08f;
        volume = 0.3f;
        noteDisplayOffset.text = noteDisplay.ToString("F2") + "";
        hitOffset.text = hit.ToString("F2") + "";
        volumeThreshold.text = volume.ToString("F2") + "";
        PlayerPrefs.SetFloat("NoteDisplayOffset", noteDisplay);
        PlayerPrefs.SetFloat("hitDelay", hit);
        PlayerPrefs.SetFloat("VolumeThreshold", volume);
    }

    public void DecreaseNoteDisplayOffset() {
        noteDisplay -= 0.01f;
        if (noteDisplay < 0) {
            noteDisplay = 0;
        }
        PlayerPrefs.SetFloat("NoteDisplayOffset", noteDisplay);
        noteDisplayOffset.text = noteDisplay.ToString("F2") + "";
    }

    public void IncreaseNoteDisplayOffset() {
        noteDisplay += 0.01f;
        if (noteDisplay > 1) {
            noteDisplay = 1;
        }
        PlayerPrefs.SetFloat("NoteDisplayOffset", noteDisplay);
        noteDisplayOffset.text = noteDisplay.ToString("F2") + "";
    }

    public void DecreaseHitOffset() {
        hit -= 0.01f;
        if (hit < 0) {
            hit = 0;
        }
        PlayerPrefs.SetFloat("hitDelay", hit);
        hitOffset.text = hit.ToString("F2") + "";
    }

    public void IncreaseHitOffset() {
        hit += 0.01f;
        if (hit > 1) {
            hit = 1;
        }
        PlayerPrefs.SetFloat("hitDelay", hit);
        hitOffset.text = hit.ToString("F2") + "";
    }

    public void DecreaseVolumeThreshold() {
        volume -= 0.01f;
        if (volume < 0) {
            volume = 0;
        }
        PlayerPrefs.SetFloat("VolumeThreshold", volume);
        volumeThreshold.text = volume.ToString("F2") + "";
    }

    public void IncreaseVolumeThreshold() {
        volume += 0.01f;
        if (volume > 1) {
            volume = 1;
        }
        PlayerPrefs.SetFloat("VolumeThreshold", volume);
        volumeThreshold.text = volume.ToString("F2") + "";
    }

    public void Test() {
        PlayerPrefs.SetString("PlayMode", "Test");
        UnityEngine.SceneManagement.SceneManager.LoadScene(5); // to SampleScene
    }

}