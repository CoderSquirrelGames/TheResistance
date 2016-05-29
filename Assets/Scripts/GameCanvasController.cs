using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameCanvasController : MonoBehaviour
{
    Text Mission, Players;
    Image SideMenu, LeaderImage;
    bool ArrowClicked, IsClicked;
    public Text Player;
    GameController GameCtrl;

    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }
    // Use this for initialization
    void Awake()
    {
        Mission = gameObject.transform.FindChild("InfoGame").transform.FindChild("MissionNumber").GetComponent<Text>();
        Players = gameObject.transform.FindChild("InfoGame").transform.FindChild("PlayersNumber").GetComponent<Text>();
        SideMenu = gameObject.transform.FindChild("SideMenu").GetComponent<Image>();
        LeaderImage = gameObject.transform.FindChild("Leader").GetComponent<Image>();

    }

    public void ArrowClick()
    {
        ArrowClicked = !ArrowClicked;
        if (ArrowClicked)
        {
            SideMenu.animation.Play("SideMenuToLeftAnimation");
        }
        else
        {
            SideMenu.animation.Play("SideMenuToRightAnimation");
        }
    }

    public void SetInfoCanvas(int mission, int players)
    {
        Mission.text = mission.ToString();
        Players.text = players.ToString();
    }


    public void SetTurn(bool state)
    {
        LeaderImage.gameObject.SetActive(state);
    }

    public void SetSideMenuPositionLeft()
    {
        ArrowClicked = true;
        SideMenu.animation.Play("SideMenuToLeftAnimation");

    }
    public void SetSideMenuPositionRight()
    {
        ArrowClicked = false;
        SideMenu.animation.Play("SideMenuToRightAnimation");


    }


    public void Ok()
    {
        if (GameCtrl.SideMenuCtrl.Chosen.Count == StaticValues.GetMissionPlayersNumber(GameCtrl.GetClient().Mission, PhotonNetwork.playerList.Length))
            GameCtrl.TeamVote();
    }

}
