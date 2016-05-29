using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SideMenuController : Photon.MonoBehaviour
{
    GameController GameCtrl;
    Vector3 SideMenuPosition, SideMenuLeftPosition;
    public Image SideMenuLeft;
    public List<GameObject> PlayersButtons;
    public List<int> Chosen = new List<int>();


    // Use this for initialization
    void Awake()
    {
        SideMenuPosition = transform.position;
        SideMenuLeftPosition = SideMenuLeft.transform.position;

    }


    public void SetSideMenuPositionLeft()
    {

        transform.position = SideMenuLeftPosition;

    }
    public void SetSideMenuPositionRight()
    {
        transform.position = SideMenuPosition;


    }



    public void GeneratePlayersGrid()
    {

        for (int p = 1; p <= PhotonNetwork.playerList.Length; p++)
        {
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                if (p == PhotonNetwork.playerList[i].ID)
                {
                    PlayersButtons[i].SetActive(true);
                    PlayersSideButtonController psbc = PlayersButtons[i].GetComponent<PlayersSideButtonController>();
                    psbc.SetValues(PhotonNetwork.playerList[i].name, PhotonNetwork.playerList[i].ID);
                }
            }
        }
    }

    public void SetLeaderImage(int playerId, bool myTurn)
    {
        for (int p = 0; p <= PhotonNetwork.playerList.Length; p++)
        {
            PlayersButtons[p].transform.FindChild("LeaderIcon").GetComponent<Image>().color = Color.gray;
            //   PlayersButtons[p].GetComponent<PlayersSideButtonController>().SetOff();
        }

        for (int p = 0; p <= PhotonNetwork.playerList.Length; p++)
        {
            if (PlayersButtons[p].GetComponent<PlayersSideButtonController>().GetID() == playerId)
            {
                PlayersButtons[p].transform.FindChild("LeaderIcon").GetComponent<Image>().color = Color.white;
                break;
            }


        }


        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PlayersButtons[i].GetComponent<Button>().interactable = myTurn;
        }
    }

    public void CleanVotes()
    {
        for (int p = 0; p < PhotonNetwork.playerList.Length; p++)
        {

            PlayersButtons[p].GetComponent<PlayersSideButtonController>().SetOff();
        }
    }

    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }

    public bool CanBeChoosed()
    {
        return Chosen.Count < GameCtrl.GetNumberOfPlayersOnMission();
    }
    public void SetChoosed(int id)
    {
        Chosen.Add(id);
    }

    public void RemoveChoosed(int id)
    {
        Chosen.Remove(id);
    }

    public List<int> GetChosenOnes()
    {
        return Chosen;
    }

    public void SetVote(int playerID, bool playerVote)
    {
        //        Debug.Log("SetVote");
        foreach (GameObject obj in PlayersButtons)
        {
            if (obj.GetComponent<PlayersSideButtonController>().ID == playerID)
            {
                obj.GetComponent<PlayersSideButtonController>().SetVote(playerVote);
            }
        }
    }



}
