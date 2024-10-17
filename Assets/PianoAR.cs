using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PianoAR : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource audioClip;
    string btnName;

    void Start()
    {
        audioClip = GetComponent<AudioSource>();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                btnName = hit.transform.name;

                switch (btnName)
                {
                    case "Do":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "Re":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "Mi":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "Fa":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "So":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "La":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    case "Si":
                        audioClip.clip = clips[0];
                        audioClip.Play();
                        break;
                    default:
                        break;
                }
            }
            else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit hit2;

                if (Physics.Raycast(ray, out hit2))
                {
                    btnName = hit2.transform.name;

                    switch (btnName)
                    {
                        case "Do":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "Re":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "Mi":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "Fa":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "So":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "La":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        case "Si":
                            audioClip.clip = clips[0];
                            audioClip.Play();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
