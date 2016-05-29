using UnityEngine;
using System.Collections;

public class CoinsController : MonoBehaviour
{

    public Rotation[] Coins;



    public void SetCoinStatus(int mission, bool success)
    {
        if (success)
        {
            Coins[mission - 1].Resistence();
        }
        else
        {
            Coins[mission - 1].Spies();
        }
    }
}
