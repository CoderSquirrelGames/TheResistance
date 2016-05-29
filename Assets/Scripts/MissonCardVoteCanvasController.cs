using UnityEngine;
using System.Collections;

public class MissonCardVoteCanvasController : MonoBehaviour
{
    public GameObject FailCard, SuccessCard;
    GameController GameCtrl;

    public void SetController(GameController gc)
    {
        GameCtrl = gc;
    }




    // Use this for initialization
    void Start()
    {
        FailCard.SetActive(false);
        SuccessCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void Vote()
    {
        SuccessCard.GetComponent<MissionVoteCardController>().Clicked = FailCard.GetComponent<MissionVoteCardController>().Clicked = false;
        FailCard.GetComponent<MissionVoteCardController>().Clicked = FailCard.GetComponent<MissionVoteCardController>().Clicked = false;
        if (GameCtrl.GetClient().MySide == StaticValues.Side.Resistance)
        {

            FailCard.SetActive(false);
            SuccessCard.SetActive(true);
        }
        else
        {
            FailCard.SetActive(true);
            SuccessCard.SetActive(true);
        }
    }

    public void Success()
    {
        GameCtrl.VoteMissionSuccess();
        if (GameCtrl.GetClient().MySide == StaticValues.Side.Spy)
        {
            FailCard.SetActive(false);
        }

        GameCtrl.GetCameraScript().VoteAnimationBack();
    }


    public void Fail()
    {
        GameCtrl.VoteMissionFail();
        if (GameCtrl.GetClient().MySide == StaticValues.Side.Spy)
        {
            SuccessCard.SetActive(false);

        }
        GameCtrl.GetCameraScript().VoteAnimationBack();
    }


}
