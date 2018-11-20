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
    Ray voiceRay;
    RaycastHit2D hit;
    int layerMask = 1 << 10;//only accept activator layer, which number is 10.
    int noteLayerMask = 1 << 9;//note layer
    int slideLayerMask = 1 << 11;//slider layer
    int totalNoteLayerMask = 5 << 9;//note and slider
    int endingLayerMask = 1 << 12;//ending note layer
    int voiceLayerMask = 1 << 13;//voice note layer

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

        float hitDelay = PlayerPrefs.GetFloat("hitDelay");//these should be set before entering the game play scene
        float noteSpeed = PlayerPrefs.GetFloat("noteSpeed");

        if (gameObject.name == "Activator" && GameManager.volume > GameManager.volumeThreshold) {//only one activator works on this
            voiceRay = new Ray(gameObject.transform.position - new Vector3(0, 2f * noteSpeed / 6, 0), new Vector3(0, 1, 0));//range of ray start: [-10/3,-1/3]
            //hit = Physics2D.GetRayIntersection(voiceRay, Mathf.Infinity, noteLayerMask);
            hit = Physics2D.Raycast(voiceRay.origin - new Vector3(0, noteSpeed * hitDelay, 1), voiceRay.direction, 4f*noteSpeed/6f, voiceLayerMask);
            if (hit) {
                //Debug.Log("hit");
                float hitPosition = hit.point.y + hitDelay * noteSpeed;
                Destroy(hit.collider.gameObject);
                Debug.Log(hitPosition);
                AddScore(System.Math.Abs(hitPosition), 2);
            }
        }

        if (!createMode && Input.touchCount>0) {
            for(int i = 0; i < Input.touchCount; i++) {
                if(Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved) {
                    
                    //Debug.Log("began");
                    //make a 2d ray from (x,-1,0) with direction of (0,1,0), then return the position of the first note it hits (or null).
                    //Debug.Log(Input.GetTouch(i).position);
                    //Debug.Log(Input.GetTouch(i).position.x);
                    //Debug.Log(gameObject.transform.position.x);
                    //Vector3 tempPosition = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0);
                    //Debug.Log(Camera.main.ScreenToWorldPoint(tempPosition));
                    ray= Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    
                    if (hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask)) {
                        if (hit.collider.gameObject.Equals(gameObject)) {
                            
                            //Debug.Log(ray.origin);
                            //Debug.Log(ray.direction);
                            //Debug.Log(hit.transform.position);
                            StartCoroutine(Pressed());
                            ray = new Ray(gameObject.transform.position-new Vector3(0, 2f * noteSpeed / 6, 0), new Vector3(0, 1, 0));
                            //hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, noteLayerMask);
                            hit = Physics2D.Raycast(ray.origin-new Vector3(0,noteSpeed*hitDelay,1), ray.direction, 4f*noteSpeed/6f, totalNoteLayerMask);
                            //Debug.Log(Input.GetTouch(i).position);
                            //***** change color
                            if (hit) {
                                //Debug.Log("hit");
                                float hitPosition = hit.point.y + hitDelay * noteSpeed;
                                if (Input.GetTouch(i).phase == TouchPhase.Began) {
                                    Destroy(hit.collider.gameObject);
                                    Debug.Log(hitPosition);
                                    if (hit.transform.tag == "note") {
                                        AddScore(System.Math.Abs(hitPosition), 0);
                                    } else if (hit.transform.tag == "slider") {
                                        AddScore(System.Math.Abs(hitPosition), 1);
                                    }
                                } else if (Input.GetTouch(i).phase == TouchPhase.Moved) {
                                    if (hit.transform.tag == "slider" && System.Math.Abs(hitPosition) < 0.6/noteSpeed*6) {
                                        Destroy(hit.collider.gameObject);
                                        Debug.Log(hitPosition);
                                        AddScore(System.Math.Abs(hitPosition), 1);
                                    }
                                }
                            }
                        }
                    }

                    //if (Input.GetTouch(i).position.y < 0.7 && Input.GetTouch(i).position.y > -0.7 && Input.GetTouch(i).position.x < gameObject.transform.position.x+0.5 && Input.GetTouch(i).position.x > gameObject.transform.position.x - 0.5) {
                    //}

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
        
        
        //if(collider.gameObject.tag=="note")
        //{
        //    //Debug.Log("enter" + key.ToString() + " " + collider.name);
        //    active = true;
        //    note = collider.gameObject;
        //}
        if(collider.gameObject.tag == "ending") {
            gameManager.GetComponent<GameManager>().GameEnd();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //active = false;
    }

    void AddScore(float hitPosition, int type) {//0 for note, 1 for slider, 2 for voice
        float speedParameter = PlayerPrefs.GetFloat("noteSpeed") / 6;
        if (type == 0 && type == 2) {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gameManager.GetComponent<GameManager>().GetScore(hitPosition));
            if((int)(hitPosition / 0.3 /speedParameter) > 2) {
                gameManager.GetComponent<GameManager>().AddStat(5 - (int)(hitPosition / 0.3 / speedParameter));
            } else {
                gameManager.GetComponent<GameManager>().AddStat(4 - (int)(hitPosition / 0.3 / speedParameter));
            }
        } else {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gameManager.GetComponent<GameManager>().GetScore(0));
            gameManager.GetComponent<GameManager>().AddStat(4);
        }
        
        
        gameManager.GetComponent<GameManager>().AddStreak();
    }


    public IEnumerator Pressed() {
        sr.color = clickedColor;
        yield return new WaitForSeconds(0.07f);
        sr.color = color;
    }
}
