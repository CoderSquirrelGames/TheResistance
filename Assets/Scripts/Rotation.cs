using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    // public ParticleSystem Particle;
    public bool Rotate = true;
    public int Type;
    public Vector3 AfterStop;
    public Quaternion newRot;
    // Use this for initialization
    void Start()
    {
        newRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotate)
        {

            switch (Type)
            {
                case 0:
                    transform.Rotate(Vector3.up * Time.deltaTime * 100);
                    break;
                case 1:
                    transform.Rotate(Vector3.down * Time.deltaTime * 100);
                    break;
                case 2:
                    transform.Rotate(Vector3.left * Time.deltaTime * 100);
                    break;
                case 3:
                    transform.Rotate(Vector3.right * Time.deltaTime * 100);
                    break;
                case 4:
                    transform.Rotate(Vector3.back * Time.deltaTime * 100);
                    break;
                case 5:
                    transform.Rotate(Vector3.forward * Time.deltaTime * 100);
                    break;
                case 6:
                    transform.Rotate(Vector3.one * Time.deltaTime * 100);
                    break;

            }

        }


    }

    public void SetRotation(bool rotate)
    {
        Rotate = rotate;
    }


    public void Resistence()
    {
        newRot = Quaternion.identity;
        newRot.x = 90;
        newRot.y = 180;
       
        PerformChange();
    }
    public void Spies()
    {
        newRot = Quaternion.identity;

        PerformChange();
    }


    public void PerformChange()
    {
        Rotate = false;
        
       transform.rotation = newRot;
    }
}


