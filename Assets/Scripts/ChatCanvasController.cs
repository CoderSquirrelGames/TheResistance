using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ChatCanvasController : MonoBehaviour
{
    public class ChatMessage
    {
        public string player;
        public string msg;
    }
    public GameObject Msg, Panel, MsgPanel;
    bool PanelSt = false;
    GameController GameCtrl;
    public InputField InputMsg;
    public List<ChatMessage> MsgList = new List<ChatMessage>();
    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowChat()
    {
        PanelSt = !PanelSt;
        Panel.SetActive(PanelSt);
    }




    public void NewMessage(string msg, int playerID)
    {
        ChatMessage msgC = new ChatMessage();
        msgC.msg = msg;
        foreach (PhotonPlayer pp in PhotonNetwork.playerList)
        {
            if (pp.ID == playerID)
            {
                msgC.player = pp.name;
                break;
            }
        }

        GameObject newMsg = (GameObject)Instantiate(Msg);
        newMsg.transform.FindChild("Player").GetComponent<Text>().text = msgC.player;
        newMsg.transform.FindChild("Msg").GetComponent<Text>().text = msgC.msg;
        newMsg.transform.SetParent(MsgPanel.transform);
    }


    public void SendNewMsg()
    {
        GameCtrl.SendChatMessage(InputMsg.text);
        InputMsg.text = "";
        
    }

}
