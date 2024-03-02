using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController playerController;

    public AudioSource audioSource;


    private void Start()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<PlayerController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!playerController.isGrounded)
            {
                if (  audioSource &&  !audioSource.isPlaying)
                {
                    audioSource?.Play();

                }
                playerController.isGrounded = true;

            }
        }
    }
}
