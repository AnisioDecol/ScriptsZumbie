using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject personagem;
    public bool test = true;
    private float smootf = 0.5f;
    private Vector2 speede;


    // Use this for initialization
    void Start()
    {

        personagem = GameObject.Find("Player");
        speede = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();

    }

    private void Follow()
    {
        Player a = GameObject.Find("Player").GetComponent<Player>();
        if (test == true)
        {

            Vector2 positionCamera = Vector2.zero;

            positionCamera.x = Mathf.SmoothDamp(transform.position.x, personagem.transform.position.x, ref speede.x, smootf);
            positionCamera.y = Mathf.SmoothDamp(transform.position.y, personagem.transform.position.y, ref speede.y, smootf);

            Vector3 newPositon = new Vector3(positionCamera.x, positionCamera.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, newPositon, Time.time);


            if (a.bauCamera == true && a.pegouBau2 == 0)
            {
                test = false;
                a.bauCamera = false;
            }
            else if (a.bauCamera == true && a.pegouBau2 == 2)
            {
                test = false;
                a.bauCamera = false;
            }

        }
        else if (test == false && a.pegouBau2 == 0)
        {
 
            Vector2 positionCamera = Vector2.zero;

            positionCamera.x = Mathf.SmoothDamp(transform.position.x, 25, ref speede.x, smootf);
            positionCamera.y = Mathf.SmoothDamp(transform.position.y, 3, ref speede.y, smootf);

            Vector3 newPositon = new Vector3(positionCamera.x, positionCamera.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, newPositon, Time.time);

            if (positionCamera.x <= 26 && positionCamera.x >= 3)
            {
                test = true;        
            }
        }
        else if (test == false && a.pegouBau2 == 2)
        {
            Vector2 positionCamera = Vector2.zero;

            positionCamera.x = Mathf.SmoothDamp(transform.position.x, 42, ref speede.x, smootf);
            positionCamera.y = Mathf.SmoothDamp(transform.position.y, 19, ref speede.y, smootf);

            Vector3 newPositon = new Vector3(positionCamera.x, positionCamera.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, newPositon, Time.time);

            if (positionCamera.x <= 43 && positionCamera.x >= 18)
            {
                test = true;
            }
        }
        
    }


 }
