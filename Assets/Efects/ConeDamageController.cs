using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDamageController : MonoBehaviour
{
    public GameObject efekt;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(efekt, transform.position,Quaternion.identity);
        }
    }

}
