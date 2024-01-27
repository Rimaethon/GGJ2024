using UnityEngine;

namespace Rimaethon.UI
{
    public class SpaceStationOrbit : MonoBehaviour
    {
        public Transform planet; // Reference to the planet's Transform.
        public float orbitSpeed = 10.0f; // Adjust this value to control the orbit speed.

        private void Update()
        {
            // Calculate the look rotation to make the bottom part of the station face the planet.
            var lookDirection = planet.position - transform.position;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            // Apply the rotation to make the bottom part face the planet.
            transform.rotation = rotation;

            // Rotate the space station around the planet.
            transform.RotateAround(planet.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}