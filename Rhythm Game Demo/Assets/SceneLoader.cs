using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadSceneSongList2(){

        SceneManager.LoadScene(4);

    }

    public void LoadSceneSongList1(){

        SceneManager.LoadScene(3);

    }

    public void LoadSceneMain(){

        SceneManager.LoadScene(0);

    }

    public void LoadScenePlay(){

        SceneManager.LoadScene(6);

    }
}
