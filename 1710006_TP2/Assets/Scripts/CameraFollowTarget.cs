using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private bool m_CanMoveLeft = true;                      // Has the camera reached the left limit
    private bool m_CanMoveRight = true;                     // Has the camera reached the right limit
    private bool m_CanMoveUp = true;                        // Has the camera reached the top limit
    private bool m_CanMoveDown = true;                      // Has the camera reached the bottom limit

    public Transform m_TopLeftLimit;                        // The position of the top left corner of the map, also used for the top and left limit
    public Transform m_BottomRightLimit;                    // The position of the bottom right corner of the map, also used for the bottom and right limit

    public bool m_CanFollow = true;                         // Used to control the Z movement of the camera



    private float m_LeftLimit;                              // Represents the left limit
    private float m_RightLimit;                             // Represents the right limit
    private float m_TopLimit;                               // Represents the top limit
    private float m_BottomLimit;                            // Represents the bottom limit

    private GameObject m_Target;                            // The player is the camera's target
    public GameObject m_LookAtTarget;                       // Used to make the camera look a little higher than the character so the player can see more

    private Vector3 m_OffSet = new Vector3();               // The camera's offset
    private const float OFF_SET_UP = 5f;                    // How high is the camera position
    private  float m_OffSetAway = 7f;                       // How far behind the character is the camera position

    private Vector3 m_AnkerPoint = new Vector3();           // Will be regulated to set the camera's anker

    private float m_FollowSpeed = 3f;                       // How fast does the camera follow the charachter


	private void Start ()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_LeftLimit = m_TopLeftLimit.position.x;
        m_RightLimit = m_BottomRightLimit.position.x;
        m_TopLimit = m_TopLeftLimit.position.z;
        m_BottomLimit = m_BottomRightLimit.position.z;

        m_AnkerPoint.y = OFF_SET_UP;
    }

	
	private void FixedUpdate ()
    {
        if (m_CanFollow)
        {

            m_OffSet = m_Target.transform.position + Vector3.up * OFF_SET_UP - m_Target.transform.forward * m_OffSetAway;
            

            CheckLimits();



            if (!m_CanMoveDown && m_CanMoveLeft && m_CanMoveRight)
            {
                BottomLimitReached();
            }
        
            if (m_CanMoveDown && !m_CanMoveLeft && m_CanMoveUp)
            {
                LeftLimitReached();
            }


            if (m_CanMoveDown && !m_CanMoveRight && m_CanMoveUp)
            {
                RightLimitReached();
            }

            if (m_CanMoveLeft && m_CanMoveRight && !m_CanMoveUp)
            {
                TopLimitReached();
            }
        
            if (m_CanMoveDown && m_CanMoveLeft && m_CanMoveRight && m_CanMoveUp)
            {
                transform.position = Vector3.Lerp(transform.position, m_OffSet, m_FollowSpeed * Time.fixedDeltaTime);
            }
            else
            {

                transform.position = Vector3.Lerp(transform.position, m_AnkerPoint, m_FollowSpeed * Time.fixedDeltaTime);
            }
        }

        transform.LookAt(m_LookAtTarget.transform);
	}

    private void CheckLimits()
    {
        if (m_Target.transform.position.x < m_LeftLimit)
        {
            m_CanMoveLeft = false;
        }
        else
        {
            m_CanMoveLeft = true;
        }

        if (m_Target.transform.position.x > m_RightLimit)
        {
            m_CanMoveRight = false;
        }
        else
        {
            m_CanMoveRight = true;
        }

        if (m_Target.transform.position.z > m_TopLimit)
        {
            m_CanMoveUp = false;
        }
        else
        {
            m_CanMoveUp = true;
        }

        if (m_Target.transform.position.z < m_BottomLimit)
        {
            m_CanMoveDown = false;
        }
        else
        {
            m_CanMoveDown = true;
        }

    }


    private void LeftLimitReached()
    {
        m_AnkerPoint.x = m_LeftLimit;
        m_AnkerPoint.z = m_Target.transform.position.z;
    }

    private void RightLimitReached()
    {
        m_AnkerPoint.x = m_RightLimit;
        m_AnkerPoint.z = m_Target.transform.position.z;
    }

    private void TopLimitReached()
    {
        m_AnkerPoint.x = m_Target.transform.position.x;
        m_AnkerPoint.z = m_TopLimit;
    }

    private void BottomLimitReached()
    {
        m_AnkerPoint.x = m_Target.transform.position.x;
        m_AnkerPoint.z = m_BottomLimit;

    }


}
