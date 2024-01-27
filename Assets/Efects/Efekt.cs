using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efekt : MonoBehaviour
{

    private void Start()
    {
        Destroy(this.gameObject, 2);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.name);

    }
}
