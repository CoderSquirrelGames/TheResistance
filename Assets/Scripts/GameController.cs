using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    Client C;
    CameraScript CamScript;
    public GameCanvasController GameCanvasCtrl;
    public SideMenuController SideMenuCtrl;
    public GameObject Vote, MissionCanvasVote, CardResistace, CardSpy;
    public MissonCardVoteCanvasController MissionCtrl;
    public CoinsController CoinsCtrl;
    public GameObject FinalCanvas;
    public ChatCanvasController ChatCtrl;
    public ShowCardsVote ShowCV;

    int NumberOfPlayersOnMission;
    public bool MyTurn;
    GameObject VoteDisplay, RejecteDisplay;
    void Awake()
    {
        CamScript = Camera.main.GetComponent<CameraScript>();

        VoteDisplay = Vote.transform.FindChild("VoteDisplay").gameObject;
        RejecteDisplay = Vote.transform.FindChild("Rejected").gameObject;
    }
    public void SetClient(Client client)
    {
        C = client;
    }
    // Use this for initialization
    void Start()
    {
        Vote.SetActive(false);


        GameCanvasCtrl.SetController(this);
        SideMenuCtrl.SetController(this);
        MissionCtrl.SetController(this);
        ChatCtrl.SetController(this);
        CamScript.SetController(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        { ChangePlayer(); }
    }

    public void SetMyCard(StaticValues.Side side)
    {

        switch (side)
        {
            case StaticValues.Side.Spy:
                CardSpy.SetActive(true);
                break;

            case StaticValues.Side.Resistance:
                CardResistace.SetActive(true);
                break;
        }
    }

    public Client GetClient()
    {
        return C;
    }

    public void SetPlayeryName(string name)
    {
        GameCanvasCtrl.Player.text = name;
    }


    public void GenerateGrid()
    {
        SideMenuCtrl.GeneratePlayersGrid();
    }


    public void SetTurn(int playerId)
    {
        if (playerId == C.GetMyId())
        {
            MyTurn = true;
        }
        else
        {
            MyTurn = false;
        }

        SideMenuCtrl.SetSideMenuPositionRight();
        Vote.SetActive(false);
        VoteDisplay.SetActive(true);
        GameCanvasCtrl.SetTurn(MyTurn);
        Vote.transform.FindChild("VoteDisplay").GetComponent<VoteCanvasController>().Reset();
        SideMenuCtrl.SetLeaderImage(playerId, MyTurn);
    }

    public void SetMissionAndPlayers(int mission, int players)
    {
        NumberOfPlayersOnMission = players;
        GameCanvasCtrl.SetInfoCanvas(mission, players);
    }

    public int GetNumberOfPlayersOnMission()
    {
        return NumberOfPlayersOnMission;
    }


    public bool IsMyTurn()
    {
        return MyTurn;
    }


    public void TeamVote()
    {

        C.PlayersSelected(SideMenuCtrl.GetChosenOnes().ToArray());
    }

    public void SetMyVote(bool MyVote)
    {
        C.Vote(MyVote);
    }

    public void VoteOnTeam(int[] ids)
    {
        GameCanvasCtrl.ArrowClick();
        Vote.SetActive(true);
        VoteDisplay.SetActive(true);
        RejecteDisplay.SetActive(false);
        VoteCanvasController voteCtrl = VoteDisplay.GetComponent<VoteCanvasController>();
        voteCtrl.SetController(this);

        voteCtrl.SetPlayersNames(ids);

    }

    public void SetPlayerVote(int playerID, bool playerVote)
    {
        SideMenuCtrl.SetVote(playerID, playerVote);
    }


    public void TeamRejected(int playerID)
    {
        if (playerID == C.GetMyId())
        {
            VoteDisplay.SetActive(false);
            RejecteDisplay.SetActive(true);
            StartCoroutine(SetOffRejectedInfo());
        }

        SetTurn(playerID);

    }

    IEnumerator SetOffRejectedInfo()
    {
        yield return new WaitForSeconds(3);
        Vote.SetActive(false);

    }


    public void VoteOnMission()
    {
        //        Debug.Log("VoteOnMission");
        Vote.SetActive(false);
        VoteDisplay.SetActive(true);
        SideMenuCtrl.CleanVotes();
        MissionCanvasVote.SetActive(true);
        MissionCtrl.Vote();
        CamScript.VoteAnimation();

    }

    public void Clean()
    {
        Vote.SetActive(false);
        VoteDisplay.SetActive(true);
        SideMenuCtrl.CleanVotes();
    }

    public void VoteMissionSuccess()
    {
        C.SetMissionVote(true);
    }



    public void VoteMissionFail()
    {
        C.SetMissionVote(false);
    }


    public void MissionStatus(int mission, bool success, bool[] votes)
    {

        CamScript.MissionAnimation();
        ShowCV.ShowVotes(votes);
        CoinsCtrl.SetCoinStatus(mission, success);

    }


    public CameraScript GetCameraScript()
    {
        return CamScript;
    }

    public void EndGame(int r)
    {
        FinalCanvas.SetActive(true);

        if (r > C.Mission / 2)
            FinalCanvas.GetComponent<FinalCanvasScripts>().Show(1);
        else
            FinalCanvas.GetComponent<FinalCanvasScripts>().Show(0);


        GameCanvasCtrl.gameObject.SetActive(false);


    }

    public void RecieveChatMessage(int playerID, string msg)
    {
        ChatCtrl.NewMessage(msg, playerID);
    }

    public void SendChatMessage(string msg)
    {
        C.SendMsg(msg);
    }




    public void ChangePlayer()
    {
        if (MyTurn)
        {
            C.ChangePlayer();
        }
    }
}
