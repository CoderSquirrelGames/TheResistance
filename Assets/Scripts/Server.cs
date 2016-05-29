using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Server : Photon.MonoBehaviour
{

    Dictionary<int, int> GetSpyNumber = new Dictionary<int, int>();
    List<int> Spies;
    int[] PlayerOnMission;
    int Mission = 0, TotalPlayers, CurrentPlayer = 1, Votes, AgreedVotes, TimeTeam;
    bool[] MissionsStats = new bool[5];
    bool[] MissionVotes = new bool[5];
    void Awake()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            this.enabled = false;
        }
        else
        {
            GetSpyNumber.Add(5, 2);
            GetSpyNumber.Add(6, 2);
            GetSpyNumber.Add(7, 3);
            GetSpyNumber.Add(8, 3);
            GetSpyNumber.Add(9, 3);
            GetSpyNumber.Add(10, 4);
            TotalPlayers = PhotonNetwork.room.maxPlayers;
            CurrentPlayer = TotalPlayers;
            Spies = new List<int>(GetSpyNumber[TotalPlayers]);

            for (int i = 0; i < GetSpyNumber[TotalPlayers] ; i++)
            {
                int p;
                do
                {
                    p = Random.Range(1, TotalPlayers + 1);
                } while (Spies.Contains(p));
                Spies.Add(p);
            }



        }
    }
    // Use this for initialization
    void Start()
    {
        SetAllPlayerCanvas();

        SetTurn();
    }
    void SetAllPlayerCanvas()
    {
        photonView.RPC(StaticValues.SETINFOCANVAS, PhotonTargets.All, Mission, StaticValues.GetMissionPlayersNumber(Mission, TotalPlayers));
        photonView.RPC(StaticValues.SETSIDECANVAS, PhotonTargets.All);

    }
    [RPC]
    void GetSide(int playerID)
    {
        if (Spies.Contains(playerID))
        {
            photonView.RPC(StaticValues.SETSIDE, PhotonTargets.All, playerID, 1);
        }
        else
        {
            photonView.RPC(StaticValues.SETSIDE, PhotonTargets.All, playerID, 0);
        }

    }
    [RPC]
    void SetTurn()
    {
        Mission++;

        AgreedVotes = Votes = 0;
        if (Mission < 6)
        {
            //Debug.Log("Set Turn");
            //Votes = AgreedVotes = 0;
            TimeTeam = 3;
            if (CurrentPlayer < TotalPlayers)
            {
                CurrentPlayer++;
            }
            else
            {
                CurrentPlayer = 1;
            }
            MissionVotes = new bool[StaticValues.GetMissionPlayersNumber(Mission, TotalPlayers)];
            photonView.RPC(StaticValues.SETTURN, PhotonTargets.All, CurrentPlayer);
            photonView.RPC(StaticValues.SETINFOCANVAS, PhotonTargets.All, Mission, StaticValues.GetMissionPlayersNumber(Mission, TotalPlayers));
        }
        else
        {
            photonView.RPC(StaticValues.ENDGAME, PhotonTargets.All, MissionsStats);
        }
    }

    [RPC]
    void SetPlayersOnMission(int[] players)
    {
        PlayerOnMission = players;
        photonView.RPC(StaticValues.VOTING, PhotonTargets.All, PlayerOnMission);
    }

    [RPC]
    void PlayerVote(int playerID, bool playerVote)
    {

        Votes++;
        if (playerVote)
        {
            AgreedVotes++;
        }

        photonView.RPC(StaticValues.INFORMVOTE, PhotonTargets.All, playerID, playerVote);

        if (Votes == TotalPlayers)
        {
            if (AgreedVotes >= Votes / 2)
            {
                photonView.RPC(StaticValues.VOTEMISSION, PhotonTargets.All, PlayerOnMission);
            }
            else
            {
                photonView.RPC(StaticValues.TEAMREJECTED, PhotonTargets.All, CurrentPlayer);
                TimeTeam--;
                if (TimeTeam < 1)
                {
                    SetTurn();
                }
            }
            AgreedVotes = Votes = 0;
        }

    }

    [RPC]
    void SetMissionVote(int playerID, bool vote)
    {
        MissionVotes[Votes] = vote;
        Votes++;
        if (vote)
            AgreedVotes++;

        if (Votes == StaticValues.GetMissionPlayersNumber(Mission, TotalPlayers))
        {

            if ((Votes - AgreedVotes) >= StaticValues.GetNumberOfFailsNeeded(Mission, TotalPlayers))
            {
                MissionsStats[Mission - 1] = false;
                photonView.RPC(StaticValues.MISSIONSTATE, PhotonTargets.All, Mission, false, MissionVotes);
            }
            else
            {
                MissionsStats[Mission - 1] = true;
                photonView.RPC(StaticValues.MISSIONSTATE, PhotonTargets.All, Mission, true, MissionVotes);
            }

        }
    }

}
