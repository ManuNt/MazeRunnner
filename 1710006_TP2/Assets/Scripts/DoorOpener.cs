using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private AudioSource m_AudioSource;      // The audio source from which the door openning noise will come from 
    public GameObject m_DoorToOpen;         // The actual door GameObject that will be triggerd
    public GameObject m_DoorBase;           // The base of the door

    private bool m_HasPlayed = false;       // Used to know if the door openning sound effect has played yet

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider aCol)
    {
        if (aCol.gameObject.tag == "Player")
        {
            if (!m_HasPlayed)
            {
                m_AudioSource.Play();
                m_HasPlayed = true;
                OpendDoor();
            }
        }
    }

    private void OpendDoor()
    {
        Destroy(m_DoorToOpen);
        Destroy(m_DoorBase);
    }

}
