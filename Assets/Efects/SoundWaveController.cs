using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveController : MonoBehaviour
{
   

    private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emission;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
       emission = particleSystem.emission;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            emission.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            emission.enabled = false;
        }
       
    }
}
