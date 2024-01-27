using System;
using UnityEngine;


public class EnemyController: MonoBehaviour
{
    public void TakeDamage(float v)
    {
        throw new NotImplementedException();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.name);

    }
}