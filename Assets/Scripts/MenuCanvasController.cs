using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MenuCanvasController : MonoBehaviour
{

    Image StatusImage, PlayersImage, GameStatus;
    //PlayersText,
    Button IncreaseBt, DecreaseBt, CreateBt, EnterBt, ConnectBt;
    InputField PlayerName;
    int NumberOfPlayers = 5;
    Dictionary<int, Sprite> DicImages = new Dictionary<int, Sprite>();
    public List<Sprite> Images, Numbers;
    public Sprite Starting;
    public GameObject Backdoor;
    bool NotStarted = true, Created = false, Entered;
    CameraScript CamScript;

    void Start()
    {
        for (int i = 5, j = 0; i < 11; i++, j++)
        {
            DicImages.Add(i, Numbers[j]);
        }
        CamScript = Camera.main.GetComponent<CameraScript>();
        StatusImage = gameObject.transform.FindChild("Display1").transform.FindChild("ConnectionStatus").GetComponent<Image>();
        PlayerName = gameObject.transform.FindChild("Display1").transform.FindChild("PlayerNameInput").GetComponent<InputField>();
        PlayersImage = gameObject.transform.FindChild("Display2").transform.FindChild("Number").GetComponent<Image>();
        //      PlayersText = gameObject.transform.FindChild("Display2").transform.FindChild("Players").GetComponent<Image>();
        IncreaseBt = gameObject.transform.FindChild("Display2").transform.FindChild("IncreaseButton").GetComponent<Button>();
        DecreaseBt = gameObject.transform.FindChild("Display2").transform.FindChild("DecreaseButton").GetComponent<Button>();
        CreateBt = gameObject.transform.FindChild("Display2").transform.FindChild("CreateGameButton").GetComponent<Button>();
        ConnectBt = gameObject.transform.FindChild("Display1").transform.FindChild("Connect").GetComponent<Button>();
        EnterBt = gameObject.transform.FindChild("Display2").transform.FindChild("EnterGameButton").GetComponent<Button>();
        GameStatus = gameObject.transform.FindChild("Display2").transform.FindChild("GameStatus").GetComponent<Image>();
    }


    void Update()
    {
        if (PhotonNetwork.connected)
        {
           // Debug.Log("Connected");
            if (PhotonNetwork.GetRoomList().Length != 0 && !Entered)
            {
              //  Debug.Log("enter");
                NotAbleToCreate();
            }
            else if (!Created)
            {
             //   Debug.Log("create");
                AbleToCreate();

            }



            if (PhotonNetwork.inRoom)
            {
                GameStatus.gameObject.SetActive(true);
                //PhotonNetwork.room.maxPlayers
                if (PhotonNetwork.room.playerCount >= PhotonNetwork.room.maxPlayers)
                {
                    if (NotStarted)
                        StartCoroutine(StartingGame());
                }
            }
        }
    }


    public void Connect()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.playerName = PlayerName.text;
            if (!(PlayerName.text.Equals("") || PlayerName.text.Equals(null)))
            {
                StatusImage.sprite = Images[1];
                PhotonNetwork.ConnectUsingSettings("DefiantResistance1.0");

                ConnectBt.interactable = false;
            }

        }
    }

    public void Create()
    {
        Entered = true;
        PlayersImage.color = Color.gray;
        //  PlayersText.color = Color.gray;
        DecreaseBt.interactable = false;
        IncreaseBt.interactable = false;
        CreateBt.interactable = false;
        PhotonNetwork.CreateRoom(StaticValues.GAMEROOM, new RoomOptions() { maxPlayers = NumberOfPlayers }, null);
        //      PhotonNetwork.CreateRoom(StaticValues.GAMEROOM, new RoomOptions() { maxPlayers = 2 }, null);

    }

    public void Enter()
    {
        Entered = true;
        PhotonNetwork.JoinRoom(StaticValues.GAMEROOM);
    }

    public void Increase()
    {
        if (NumberOfPlayers < 10)
        {
            NumberOfPlayers++;
            UpdatePlayerImage(NumberOfPlayers);
        }
    }

    public void Decrease()
    {
        if (NumberOfPlayers > 5)
        {
            NumberOfPlayers--;
            UpdatePlayerImage(NumberOfPlayers);
        }
    }

    void UpdatePlayerImage(int n)
    {
        PlayersImage.sprite = DicImages[n];
    }

    void OnJoinedRoom()
    {
    }



    public void NotAbleToCreate()
    {
        PlayersImage.color = Color.gray;
        //  PlayersText.color = Color.gray;
        DecreaseBt.interactable = false;
        IncreaseBt.interactable = false;
        CreateBt.interactable = false;
        EnterBt.interactable = true;
    }



    IEnumerator StartingGame()
    {
        NotStarted = false;

        GameStatus.sprite = Starting;
        Backdoor.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1);
        //  Camera.main.animation.Play("EnterGameAnimation");
        CamScript.EnterGameAnimation();
    }
    public void AbleToCreate()
    {
        Created = true;
        PlayersImage.color = Color.white;
        //       PlayersText.color = Color.white;
        DecreaseBt.interactable = true;
        IncreaseBt.interactable = true;
        CreateBt.interactable = true;
        EnterBt.interactable = false;
    }
}