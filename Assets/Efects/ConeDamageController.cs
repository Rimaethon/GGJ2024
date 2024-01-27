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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ExpandDamageArea());
        }
    }

    IEnumerator ExpandDamageArea()
    {
        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * damageRate;
            UpdateDamageArea();
            yield return null;
        }
    }

    void UpdateDamageArea()
    {
        Vector3 coneDirection = transform.forward; // Karakterin yüzünün normal vektörü

        // Koni þeklinde hasar alanýný oluþtur
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, currentRadius, coneDirection, 0f, enemyLayer);

        foreach (RaycastHit hit in hits)
        {
            // Hasar alabilen düþmanlara hasar ver
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(10f);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Koni gösterge çizimi
        Gizmos.color = Color.red;

        float angleIncrement = 360f / segments;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward);

        for (float angle = 0f; angle < 360f; angle += angleIncrement)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * currentRadius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * currentRadius;

            Vector3 startPoint = transform.position + rotation * new Vector3(x, y, 0f);
            Vector3 endPoint = transform.position + rotation * new Vector3(x, y, currentRadius);

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}
