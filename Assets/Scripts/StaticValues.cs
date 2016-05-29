using UnityEngine;
using System.Collections;

public class StaticValues : MonoBehaviour
{
    public enum Side
    {
        Resistance, Spy
    }
    public static string GAMEROOM = "DEFIANTRESISTANCE";
    public static string SETINFOCANVAS = "SetInfoCanvas";
    public static string SETSIDECANVAS = "SetSideCanvas";
    public static string SETSIDE = "SetSide";
    public static string GETSIDE = "GetSide";
    public static string SETTURN = "GetTurn";
    public static string GETTURN = "SetTurn";
    public static string VOTING = "PlayerOnMissionVoting";
    public static string SETMISSION = "SetPlayersOnMission";
    public static string PLAYERVOTE = "PlayerVote";
    public static string INFORMVOTE = "InformPlayerVote";
    public static string TEAMREJECTED = "TeamRejected";
    public static string VOTEMISSION = "VoteMission";
    public static string SETMISSIONVOTE = "SetMissionVote";
    public static string MISSIONSTATE = "MissionState";
    public static string ENDGAME = "EndGame";
    public static string CHATMESSAGE = "RecieveChatMessage";
    public static int GetMissionPlayersNumber(int mission, int totalPlayers)
    {
        switch (mission)
        {
            case 1:
                switch (totalPlayers)
                {
                    case 5:
                    case 6:
                    case 7:
                        return 2;
                    case 8:
                    case 9:
                    case 10:
                        return 3;
                }

                break;
            case 2:
                switch (totalPlayers)
                {
                    case 5:
                    case 6:
                    case 7:
                        return 3;
                    case 8:
                    case 9:
                    case 10:
                        return 4;
                }
                break;
            case 3:
                switch (totalPlayers)
                {
                    case 5:
                        return 2;
                    case 6:
                        return 4;
                    case 7:
                        return 3;
                    case 8:
                    case 9:
                    case 10:
                        return 4;
                }
                break;
            case 4:
                switch (totalPlayers)
                {
                    case 5:
                    case 6:
                        return 3;
                    case 7:
                        return 2;
                    case 8:
                    case 9:
                    case 10:
                        return 5;
                }
                break;
            case 5:
                switch (totalPlayers)
                {
                    case 5:
                        return 4;
                    case 6:
                    case 7:
                        return 4;
                    case 8:
                    case 9:
                    case 10:
                        return 5;
                }
                break;

        }
        return 0;
    }

    public static int GetNumberOfFailsNeeded(int mission, int totalPlayers)
    {
        if (mission == 4 && totalPlayers > 6)
        {
            return 2;
        }
        return 1;
    }

}
