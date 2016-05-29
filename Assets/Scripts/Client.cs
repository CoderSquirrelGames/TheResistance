
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Client : Photon.MonoBehaviour
{
    //public GameCanvasController CanvasController;
    //public SideMenuController SideController;
    public GameController GameCtrl;
    public StaticValues.Side MySide;
    public int Mission;
    //bool MyTurn = false;
    int MyId;
    // Use this for initialization
    void Start()
    {

        MyId = PhotonNetwork.player.ID;
        GameCtrl.SetPlayeryName(PhotonNetwork.player.name);
        photonView.RPC(StaticValues.GETSIDE, PhotonTargets.MasterClient, MyId);
        GameCtrl.SetClient(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [RPC]
    void SetInfoCanvas(int mission, int players)
    {
        Mission = mission;
        GameCtrl.SetMissionAndPlayers(mission, players);

    }

    [RPC]
    void SetSideCanvas()
    {
        GameCtrl.GenerateGrid();
    }

    [RPC]
    void SetSide(int playerID, int side)
    {
        StaticValues.Side myside;
        if (MyId == playerID)
        {
            if (side == 0)
            {
                myside = StaticValues.Side.Resistance;
            }
            else
            {
                myside = StaticValues.Side.Spy;
                GameCtrl.ChatCtrl.gameObject.SetActive(true);
            }
            MySide = myside;
            GameCtrl.SetMyCard(MySide);
        }
    }


    [RPC]
    void GetTurn(int playerID)
    {

        GameCtrl.SetTurn(playerID);

    }

    public void PlayersSelected(int[] ids)
    {
        photonView.RPC(StaticValues.SETMISSION, PhotonTargets.MasterClient, ids);
    }

    [RPC]
    void PlayerOnMissionVoting(int[] ids)
    {
        GameCtrl.VoteOnTeam(ids);
    }

    [RPC]
    void RecieveChatMessage(string msg, int playerID)
    {
        if (MySide == StaticValues.Side.Spy)
        {
            GameCtrl.RecieveChatMessage(playerID, msg);
        }
    }

    public int GetMyId()
    {
        return MyId;
    }


    public void Vote(bool MyVote)
    {
        photonView.RPC(StaticValues.PLAYERVOTE, PhotonTargets.MasterClient, MyId, MyVote);
    }


    [RPC]
    void InformPlayerVote(int playerID, bool playerVote)
    {
        GameCtrl.SetPlayerVote(playerID, playerVote);
    }

    [RPC]
    void TeamRejected(int playerID)
    {


        GameCtrl.TeamRejected(playerID);

    }

    [RPC]
    void VoteMission(int[] playersOnMission)
    {
        // Debug.Log("Player VoteMission");

        if (playersOnMission.Contains(MyId))
        {
            //    Debug.Log("I'm on a mission");
            GameCtrl.VoteOnMission();
        }
        else
        {
            //   Debug.Log("I'm on a mission");
            GameCtrl.Clean();
        }
    }

    [RPC]
    void EndGame(bool[] MissionsStats)
    {
        int resis = 0;
        for (int m = 0; m < MissionsStats.Length; m++)
        {
            if (MissionsStats[m])
            {
                resis++;
            }
        }

        GameCtrl.EndGame(resis);

    }

    public void SendMsg(string msg)
    {
        photonView.RPC(StaticValues.CHATMESSAGE, PhotonTargets.All, msg, MyId);
    }

    public void SetMissionVote(bool vote)
    {
        photonView.RPC(StaticValues.SETMISSIONVOTE, PhotonTargets.MasterClient, MyId, vote);
    }

    [RPC]
    void MissionState(int mission, bool success, bool[] votes)
    {
        GameCtrl.MissionStatus(mission, success, votes);



    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }


    public void ChangePlayer()
    {
        photonView.RPC(StaticValues.GETTURN, PhotonTargets.MasterClient);
    }
}
