using UnityEngine;
using System.Collections;

public class VotesClick : MonoBehaviour
{
    public ShowCardsVote votes;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        audio.Play();
        votes.Show(audio);
    }
}
