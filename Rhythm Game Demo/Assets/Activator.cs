using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public KeyCode key;
    bool active = false;
    GameObject note;
    GameObject gameManager;
    SpriteRenderer sr;
    Color color;//original color
    Color clickedColor;
    public bool createMode;//for creating maps
    public GameObject cloneNote;//for creating maps

    Ray ray;//for tapping
    RaycastHit2D hit;
    int layerMask = 1 << 10;//only accept activator layer, which number is 10.

	void Awake () {
        
    }

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager");
        color = sr.color;
        clickedColor = new Color(1f, 1f, 1f);
        //Debug.Log(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        //if (!createMode && Input.GetKeyDown(key)) {
        //    StartCoroutine(Pressed());
        //    if (active) {
        //        Destroy(note);
        //        gameManager.GetComponent<GameManager>().AddStreak();
        //        active = false;
        //        AddScore();
        //    } else {//hit at a bad timing, which leads to resetting combo
        //        gameManager.GetComponent<GameManager>().ResetStreak();
        //    }
        //}


        if(!createMode && Input.touchCount>0) {
            for(int i = 0; i < Input.touchCount; i++) {
                if(Input.GetTouch(i).phase == TouchPhase.Began) {
                    //Debug.Log("began");
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    //Debug.Log(Input.GetTouch(i).position);
                    //***** change color
                    //if (Physics2D.Raycast(ray, out hit, Mathf.Infinity, layerMask)) 
                    if(hit=Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask)){
                        //Debug.Log("hit");
                        if (hit.transform.tag=="activator") {
                            //Debug.Log("activator");
                            if (hit.collider.gameObject.Equals(gameObject)) {
                                //Debug.Log("equal");
                                StartCoroutine(Pressed());
                                if (active) {
                                    //Debug.Log(note.gameObject.name);
                                    Destroy(note);
                                    AddScore();
                                    gameManager.GetComponent<GameManager>().AddStreak();
                                    active = false;
                                } else {//hit at a bad timing, which leads to resetting combo
                                    Debug.Log("bad timing");
                                    gameManager.GetComponent<GameManager>().ResetStreak();
                                }
                            }
                        }
                    }
                }
            }
        }

        else if(createMode) {
            if (Input.GetKeyDown(key)) {
                Instantiate(cloneNote, transform.position, Quaternion.identity);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collider)//*********** maybe needs to be improved when two notes are closed to each other
    {
        
        
        if(collider.gameObject.tag=="note")
        {
            //Debug.Log("enter" + key.ToString() + " " + collider.name);
            active = true;
            note = collider.gameObject;
        }
        else if(collider.gameObject.tag == "ending") {
            gameManager.GetComponent<GameManager>().GameEnd();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        active = false;
    }

    void AddScore() {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gameManager.GetComponent<GameManager>().GetScore());//fixed score
    }


    public IEnumerator Pressed() {
        sr.color = clickedColor;
        yield return new WaitForSeconds(0.07f);
        sr.color = color;
    }
}
