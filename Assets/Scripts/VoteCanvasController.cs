using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class VoteCanvasController : MonoBehaviour
{
    Button BT_Agreed, BT_Disagreed;
    public List<Text> Players = new List<Text>();

    GameController GameCtrl;
    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }
    // Use this for initialization
    void Awake()
    {
        BT_Agreed = gameObject.transform.FindChild("Agreed").GetComponent<Button>();
        BT_Disagreed = gameObject.transform.FindChild("Disagreed").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Agreed()
    {
        BT_Agreed.interactable = false;
        BT_Disagreed.interactable = false;
        GameCtrl.SetMyVote(true);
    }


    public void Disagreed()
    {
        BT_Agreed.interactable = false;
        BT_Disagreed.interactable = false;
        GameCtrl.SetMyVote(false);

    }


    public void SetPlayersNames(int[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {

            Players[i].text = GetNameById(ids[i]);
        }

    }

    public string GetNameById(int id)
    {
        string pName = "";
        foreach (PhotonPlayer pp in PhotonNetwork.playerList)
        {
            if (pp.ID == id)
            {
                pName = pp.name;
            }
        }
        return pName;
    }

    public void Reset()
    {
        for (int i = 0; i < Players.Count; i++)
        {

            Players[i].text = "";

        }

        if ((BT_Agreed == null) || (BT_Disagreed == null))
        {
            BT_Agreed = gameObject.transform.FindChild("Agreed").GetComponent<Button>();
            BT_Disagreed = gameObject.transform.FindChild("Disagreed").GetComponent<Button>();
        }
        BT_Agreed.interactable = true;
        BT_Disagreed.interactable = true;
    }

}
