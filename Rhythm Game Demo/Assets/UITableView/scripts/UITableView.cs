using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITableView : MonoBehaviour {
	public RectTransform tableView;
	public RectTransform view;
    public GameObject tableViewCell;

	public Sprite icon;

	public int cells;

    //Var for access database
    private string Url = "https://i.cs.hku.hk/~wzgao/Get_leaderboard.php";
    public int Song_id;

    public void CreateMainForm(int song_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("song_ID", song_id);
        StartCoroutine(SendPost(Url, form));
    }

    IEnumerator SendPost(string url, WWWForm form)
    {
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error");
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);

            if (www.text == "empty")
            {
                Debug.Log("empty!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                GameObject cell = Instantiate(tableViewCell);
                cell.transform.SetParent(view.transform, false);
                UITableViewCell tableViewCellScript = cell.GetComponent<UITableViewCell>();
                tableViewCellScript.UserRank = "No ";
                tableViewCellScript.UserName = "Record ";
                tableViewCellScript.SetIcon(icon);
                tableViewCellScript.UserScore = 0;
            }
            else
            {

            }
        }
    }

    // Use this for initialization
    void Start () {
		float height = tableView.rect.height;

		if (height < 100 * cells)
		{
			height = 100 * cells;
		}

		view.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		view.SetPositionAndRotation(new Vector3(view.position.x, -height / 2, 0), Quaternion.Euler(0, 0, 0));

        Song_id = 555;//test
        CreateMainForm(Song_id);

        //for (int i = 0; i < cells; i++)
        //{
        //	GameObject cell = Instantiate(tableViewCell);
        //	cell.transform.SetParent(view.transform, false);

        //	UITableViewCell tableViewCellScript = cell.GetComponent<UITableViewCell>();
        //	tableViewCellScript.UserRank = (i + 1).ToString();
        //	tableViewCellScript.UserName = "Player " + (i + 1);
        //	if (i+1 < 4) tableViewCellScript.SetIcon(icon);
        //	tableViewCellScript.UserScore = (i + 1) * 3;
        //}

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
