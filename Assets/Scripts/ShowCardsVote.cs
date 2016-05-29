using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ShowCardsVote : MonoBehaviour
{

    public GameObject FailPrefab, SuccessPrefab, Spawn, Spot;
    public List<Image> Imgs;
    public List<Sprite> Prefabs;
    AudioSource thisAudio;
    bool[] Votes;

    public void ShowVotes(bool[] votes)
    {
        Votes = votes;

    }

    public void Show(AudioSource audio)
    {
        Spot.SetActive(true);
        StartCoroutine(ShowCards(audio));
    }



    public void SetImage(bool v)
    {
        foreach (Image i in Imgs)
        {
            if (!i.gameObject.activeSelf)
            {
                if (v)
                {
                    i.sprite = Prefabs[1];
                }
                else
                {
                    i.sprite = Prefabs[0];
                }
                i.gameObject.SetActive(true);


                break;
            }
        }
    }


    IEnumerator ShowCards(AudioSource audio)
    {
        thisAudio = audio;
        GameObject go;
        foreach (bool v in Votes)
        {
            if (v)
            {
                go = Instantiate(SuccessPrefab) as GameObject;
            }
            else
            {
                go = Instantiate(FailPrefab) as GameObject;
            }
            go.transform.SetParent(Spawn.transform);
            SetImage(v);
            yield return new WaitForSeconds(3);
            Destroy(go);
        }


        StartCoroutine(SetBackCamera());

    }


    IEnumerator SetBackCamera()
    {
        
        yield return new WaitForSeconds(3);
        thisAudio.Stop();
        foreach (Image i in Imgs)
        {
            i.gameObject.SetActive(false);
        }
        Camera.main.GetComponent<Animation>().Play("CameraMissionBackAnimation");
        Spot.SetActive(false);
    }



    public void StorAudio()
    {

    }
}

