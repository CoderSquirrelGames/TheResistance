using UnityEngine;
using System.Collections;

public class MissionVoteCardController : MonoBehaviour
{
    public int Type;
    public MissonCardVoteCanvasController MissionCtrl;
    public bool Clicked = false;
    // Use this for initialization
    void Start()
    {

    }


    void OnMouseDown()
    {
        if (!Clicked)
        {
            Clicked = true;

            if (Type == 0)
            {

                MissionCtrl.Success();
            }
            else
            {

                MissionCtrl.Fail();
            }
       
       
        
        }
    }
}
