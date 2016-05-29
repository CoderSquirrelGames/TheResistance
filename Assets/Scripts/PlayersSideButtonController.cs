using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayersSideButtonController : MonoBehaviour
{
    public int ID;
    public string PlayerName;
    bool Choosed = false;
    public SideMenuController SideCtrl;
    public Sprite[] Images;
    public Image VoteIcon;
    // Use this for initialization
    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Choose()
    {
        Button b = transform.GetComponent<Button>();
        if (Choosed)
        {
            b.GetComponent<Image>().color = Color.gray;
            Choosed = !Choosed;
            SideCtrl.RemoveChoosed(ID);
        }
        else if (SideCtrl.CanBeChoosed())
        {
            b.GetComponent<Image>().color = Color.black;
            Choosed = !Choosed;
            SideCtrl.SetChoosed(ID);
        }
    }




    public void SetValues(string Name, int id)
    {
        PlayerName = Name;
        ID = id;
        transform.FindChild("Text").GetComponent<Text>().text = PlayerName;
    }

    public int GetID()
    {
        return ID;
    }

    public void SetVote(bool vote)
    {
        //        Debug.Log("Player set vote");
        if (vote)
        {
            VoteIcon.sprite = Images[0];
        }
        else
        {
            VoteIcon.sprite = Images[1];
        }
    }
    public void SetOff()
    {
        if (VoteIcon == null)
        {
            Debug.Log(name + " " + gameObject.transform.parent.name);
        }
        //Debug.Log(VoteIcon);
        //Debug.Log(Images);
        //Debug.Log(Images[2]);
        VoteIcon.sprite = Images[2];
        transform.GetComponent<Button>().GetComponent<Image>().color = Color.gray;
        Choosed = false;
        SideCtrl.RemoveChoosed(ID);
    }
}
