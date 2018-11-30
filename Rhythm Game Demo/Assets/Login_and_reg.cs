using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login_and_reg : MonoBehaviour {
    public InputField ID;
    public InputField Pwd;
    public TextMeshProUGUI ShowText;
    //public Text ShowText;
    private string Url = "https://i.cs.hku.hk/~wzgao/Login_and_reg.php";
    bool action;
    private GameObject Login_btn, Reg_btn;

    // Use this for initialization
    void Start () {
        Login_btn = GameObject.Find("Button_Login");
        Reg_btn = GameObject.Find("Button_Reg");
        PlayerPrefs.SetString("UserID", "***");

        Login_btn.GetComponent<Button>().onClick.AddListener(Button_Login);
        Reg_btn.GetComponent<Button>().onClick.AddListener(Button_Reg);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Button_Login()
    {
        ShowText.text = "login";
        action = true;
        if (ID.text != "***") {
            CreateMainForm(ID.text, Pwd.text, action);
        }
        else
        {
            ShowText.text = "The user name cannot be \"***\"";
        }
    }

    public void Button_Reg()
    {
        ShowText.text = "reg";
        action = false;
        if (ID.text != "***")
        {
            CreateMainForm(ID.text, Pwd.text, action);
        }
        else
        {
            ShowText.text = "The user name cannot be \"***\"";
        }
        
    }

    public void CreateMainForm(string id, string pwd, bool action)
    {
        WWWForm form = new WWWForm();
        form.AddField("usr", id);
        form.AddField("pwd", pwd);
        if (action == true)
        {
            form.AddField("act", "login");
        }
        else
        {
            form.AddField("act", "reg");
        }
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

            if (action == false)                                //Sign up
            {
                if (www.text == "success")
                {
                    ShowText.text = "Congratulations! ";
                    PlayerPrefs.SetString("UserID", ID.text);
                    SceneManager.LoadScene(4);
                }
                else if (www.text == "exist")
                {
                    ShowText.text = "Sorry, this name has been occupied! Please Try Again!";
                }
                else ShowText.text = www.text;
            }
            else                                                //Login
            {
                if (www.text == "success")
                {
                    ShowText.text = "Login Successful!";
                    PlayerPrefs.SetString("UserID", ID.text);
                    SceneManager.LoadScene(4); 
                }
                else if (www.text == "fail")
                {
                    ShowText.text = "Your id or password is incorrect! Please Try Again!";
                }
            }
        }
    }
}
