using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_and_reg : MonoBehaviour {
    public InputField ID;
    public InputField Pwd;
    public Text ShowText;
    private string Url = "https://i.cs.hku.hk/~wzgao/Login_and_reg.php";
    bool action;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Button_Login()
    {
        action = true;
        CreateMainForm(ID.text, Pwd.text, action);
    }

    public void Button_Reg()
    {
        action = false;
        CreateMainForm(ID.text, Pwd.text, action);
    }

    public void CreateMainForm(string id, string pwd, bool action)
    {
        WWWForm form = new WWWForm();
        form.AddField("usr", name);
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
                }  else if (www.text == "exist")
                {
                    ShowText.text = "Sorry, this name has been occupied!";
                }
            }
            else                                                //Login
            {
                if (www.text == "success")
                {
                    ShowText.text = "Login Successful!";
                }
                else if (www.text == "fail")
                {
                    ShowText.text = "Your id or password is incorrect! Please Try Again!";
                }
            }

            Invoke("ShowTextNull", 1);
        }
    }

    void ShowTextNull()
    {
        ShowText.text = "";
    }
}
