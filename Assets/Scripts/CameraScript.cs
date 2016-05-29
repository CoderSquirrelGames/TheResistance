using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{


    public GameObject MenuCanvas, OnlineControllers, GameCanvas;
    GameController GameCtrl;
    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void EnterGameAnimation()
    {
        Camera.main.GetComponent<Animation>().Play("SideAnimation");
        StartCoroutine(EnableScripts());
    }

    //public void EnterGameAnimation()
    //{
    //    Camera.main.GetComponent<Animation>().Play("EnterGameAnimation");


    //}

    IEnumerator EnableScripts()
    {
        yield return new WaitForSeconds(3);
        MenuCanvas.SetActive(false);
        OnlineControllers.SetActive(true);
        GameCanvas.SetActive(true);
    }



    public void VoteAnimation()
    {
        Camera.main.GetComponent<Animation>().Play("VoteAnimation");
    }

    public void VoteAnimationBack()
    {
        Camera.main.GetComponent<Animation>().Play("VoteAnimationBack");
    }


    public void MissionAnimation()
    {
        Camera.main.GetComponent<Animation>().Play("CameraMissionAnimation");
        if (GameCtrl.MyTurn)
            StartCoroutine(ChangePlayer());
    }

    public void AfterBackToTable()
    {

    }

    IEnumerator ChangePlayer()
    {
        yield return new WaitForSeconds(15);
        GameCtrl.ChangePlayer();
    }
}
