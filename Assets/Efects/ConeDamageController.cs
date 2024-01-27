using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDamageController : MonoBehaviour
{
    public float maxRadius = 5f;
    public float damageRate = 10f;
    public LayerMask enemyLayer;
    public int segments = 20;

    private float currentRadius = 0f;


    public GameObject efekt;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(efekt, transform.position,Quaternion.identity);
        }
    }

}
