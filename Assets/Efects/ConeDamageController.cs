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
            Instantiate(efekt, transform.position, Quaternion.Euler(transform.eulerAngles));   // Quaternion.Euler(new Vector3(transform.parent.eulerAngles.x + 45f, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z-180)
        }
    }

}
