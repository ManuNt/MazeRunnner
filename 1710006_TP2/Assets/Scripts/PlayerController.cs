using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int m_Hp = 100;                                      // The character's HP

    private ParticleSystem m_MuzzleFlashParticleSystem;         // Used to spawn particle effects when shooting
    public GameObject m_MuzzleFlash;                            // The end point where the particle effects will go from

    public AudioSource m_GunShot;                               // The gun's audio source

    public float m_MovementSpeed = 10f;                         // The character's speed
    public bool m_IsMoving = false;                             // Used to regulate the movement

    private Vector3 m_TargetPosition;                           // Used to know where did the player click in order to move the character to that position if applicable
    private Vector3 m_RotationTarget;                           // Used to rotate the character towards the target
    public Animator m_Anim;                                     // The characters animations

    private Rigidbody m_Rb;                                     // Using the physics to move the character

    private const float MAX_RAY_DISTANCE = 1000f;               // How far can the raycast can be shot

    public Transform m_SpawnPoint;                              // Where will the player starts its journey

	private void Start ()
    {
        m_Rb = GetComponent<Rigidbody>();
        transform.position = m_SpawnPoint.position;

        m_MuzzleFlashParticleSystem = m_MuzzleFlash.GetComponent<ParticleSystem>();
    }


    private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckInput();
        }

        if (m_IsMoving)
        {
            m_Anim.SetBool("isRunning", true);

            if (Vector3.Distance(transform.position, m_TargetPosition) < 1)
            {
                m_IsMoving = false;
            }
        }
        else
        {
            m_Anim.SetBool("isRunning", false);
        }    

    }

    private void FixedUpdate()
    {
        if (m_IsMoving)
        {
            MovePlayer();
        }
        else
        {
            m_Rb.velocity = Vector3.zero;
        }
    }

    private void CheckInput()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;

        

        if (Physics.Raycast(mouseRay, out mouseHit, MAX_RAY_DISTANCE, LayerMask.GetMask("Clickable")))
        {
            m_RotationTarget = mouseHit.point;
            m_RotationTarget.y = transform.position.y;
            transform.LookAt(m_RotationTarget);

            if (mouseHit.transform.tag == "Ground")
            {
                m_IsMoving = true;
                m_TargetPosition = mouseHit.point;
            }
            else if (mouseHit.transform.tag == "Guess")
            {
                m_IsMoving = false;

                Ray bodyRay = new Ray(transform.position, transform.forward);
                RaycastHit bodyHit;
                if (Physics.Raycast(bodyRay, out bodyHit, MAX_RAY_DISTANCE))
                {
                    if (bodyHit.transform.tag == "Guess") 
                    {

                        mouseHit.transform.gameObject.GetComponent<Renderer>().material = mouseHit.transform.gameObject.GetComponentInParent<GuessGame>().ChangeColor(mouseHit.transform.gameObject.GetComponent<Renderer>(), mouseHit.transform.gameObject.GetComponent<MyColour>());
                        Shoot();

                    }
                    
                }
            }
        }

    }

    private void MovePlayer()
    {
        Vector3 movement = m_TargetPosition - transform.position;
        movement.Normalize();

        m_Rb.velocity = movement * m_MovementSpeed;
        
    }

    private void Shoot()
    {
        m_GunShot.Play();
        m_Anim.SetTrigger("Shoot");
        m_MuzzleFlashParticleSystem.Play();
    }

}
