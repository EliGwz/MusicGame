using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITableViewCell : MonoBehaviour
{
	public Text rank;
	public Text userid;
	public Text score;
	public Image icon;
	public Image line;

	private string userRank;

	public string UserRank
	{
		set
		{
			userRank = value;
			rank.text = userRank;
		}
	}

	private string userName;

	public string UserName
	{
		set
		{
			userName = value;
			userid.text = userName;
		}
	}


	//private string bookCategory;

	//public string BookCategory
	//{
	//	set
	//	{
	//		bookCategory = value;
	//	}
	//}


	private string userScore;

	public string UserScore
	{
		set
		{
			userScore = value;
			score.text = userScore;
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	//public void Read_Action()
	//{
	//	print("Click " + userRank);
	//}

	public void SetIcon(Sprite ico)
	{
		icon.sprite = ico;
        var r = icon.color.r;
        var g = icon.color.g;
        var b = icon.color.b;
        icon.color = new Color(r, g, b, 255);
	}
}
