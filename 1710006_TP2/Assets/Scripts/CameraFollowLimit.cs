using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLimit : MonoBehaviour
{
    CameraFollowTarget m_Camera;    // Collider that will be triggered when the player will have reached one of the limits on the map

    private void Start()
    {
        m_Camera = Camera.main.GetComponent<CameraFollowTarget>();
    }

    private void OnTriggerEnter(Collider aCollider)
    {
        if (aCollider.gameObject.tag == "Player")
        {
            m_Camera.m_CanFollow = false;
        }
    }

    private void OnTriggerExit(Collider aCollider)
    {
        if (aCollider.gameObject.tag == "Player")
        {
            m_Camera.m_CanFollow = true;
        }
    }

}
