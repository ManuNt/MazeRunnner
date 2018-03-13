using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFeedback : MonoBehaviour                   // If the player hits a wall, it will stop moving
{
    private void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.collider.gameObject.tag == "Player")
        {
            aCollision.collider.gameObject.GetComponent<PlayerController>().m_IsMoving = false;
        }
    }
}
