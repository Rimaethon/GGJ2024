using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafespeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;
    public AudioSource audioSource;

    private void Awake()
    {
            audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }

            //isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-hips.transform.right * strafespeed);
            //isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward * speed);
            //isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.right * strafespeed);
            //isMoving = true;
        }
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }

            //isMoving = true;
        }

        //if (isMoving && !audioSource.isPlaying)
        //{
        //    audioSource.Play();
        //}
        //else if (!isMoving && audioSource.isPlaying)
        //{
        //    audioSource.Stop();
        //}
    }
}