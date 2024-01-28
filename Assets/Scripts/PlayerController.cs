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


    
    private void FixedUpdate()
    {
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
            
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-hips.transform.right * strafespeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.right * strafespeed);
        }
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }
}
