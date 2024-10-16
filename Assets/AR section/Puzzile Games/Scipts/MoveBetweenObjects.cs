using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.AR
{
    public class MoveBetweenObjects : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The base position to maintain height from")]
        public Transform basePosition;
        //
        [Tooltip("Height above the base position")]
        public float heightAboveBase = 10.0f;
        //
        public Transform objectA;  // Object A to move towards
        public Transform objectB;  // Object B to move towards after reaching A
        public float speed = 5f;   // Speed of movement
        private Transform target;  // Current target object

        void Start()
        {
            // Set the initial target to object A
            target = objectA;
        }

        void Update()
        {
            // Move the object towards the target in X and Y axes
            Vector3 targetPosition = new Vector3(target.position.x, basePosition.transform.position.y + heightAboveBase, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the object has reached the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Switch target: if it reached A, switch to B, if it reached B, switch to A
                target = target == objectA ? objectB : objectA;
            }
        }
    }
}