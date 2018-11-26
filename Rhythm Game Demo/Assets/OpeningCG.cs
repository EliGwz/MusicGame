using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningCG : MonoBehaviour {
    private VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
        videoPlayer = this.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
	}
	
    void EndReached(VideoPlayer vPlayer)
    {
        Debug.Log("CGEnd");
        SceneManager.LoadScene(1);
    }

	// Update is called once per frame
	void Update () {
        
    }
}
