using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FinalCanvasScripts : MonoBehaviour
{
    public List<Sprite> Winners;
    public Image Winner;
    public GameObject Panel;
    // Use this for initialization
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StarNewGame()
    {
        PhotonNetwork.Disconnect();
        Application.LoadLevel("Resistance");
    }

    public void Show(int winner)
    {

        Winner.sprite = Winners[winner];



        Panel.SetActive(true);
    }
}
