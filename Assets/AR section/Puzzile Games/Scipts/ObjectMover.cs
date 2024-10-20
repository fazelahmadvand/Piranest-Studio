﻿using UnityEngine;

namespace Piranest.AR
{
    public class ObjectMover : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The base position to maintain height from")]
        public Transform basePosition;

        [Tooltip("First target")]
        public Transform targetA;

        [Tooltip("Second target")]
        public Transform targetB;

        [Header("Movement Settings")]
        [Tooltip("Movement speed")]
        public float moveSpeed = 5.0f;

        [Tooltip("Height above the base position")]
        public float heightAboveBase = 10.0f;

        private Transform currentTarget;
        private bool movingToA = true; // Toggle to decide which target to move to

        void Start()
        {
            if (basePosition == null || targetA == null || targetB == null)
            {
                Debug.LogError("BasePosition, TargetA, and TargetB must be assigned.");
                enabled = false;
                return;
            }

            // Initialize current target
            currentTarget = targetA;
            SetHeightFromBase();
        }

        void Update()
        {
            MoveTowardsTarget();
            CheckForTargetReached();
        }

        /// <summary>
        /// Sets the object's height based on the base position and heightAboveBase.
        /// </summary>
        void SetHeightFromBase()
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = basePosition.localPosition.y + heightAboveBase;
            transform.localPosition = newPosition;
        }

        /// <summary>
        /// Moves the object towards the current target while maintaining the specified height.
        /// </summary>
        void MoveTowardsTarget()
        {
            Vector3 targetPosition = new Vector3(
                currentTarget.localPosition.x,
                basePosition.localPosition.y + heightAboveBase,
                currentTarget.localPosition.z
            );

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Increases the height above the base position if the provided height value is 2 or more.
        /// </summary>
        /// <param name="height">Height value to check.</param>
        public void IncreaseHeight(int height)
        {
            if (height >= 2)
            {
                heightAboveBase += 0.035f;
                SetHeightFromBase();
            }
        }

        /// <summary>
        /// Checks if the object has reached the current target and switches to the other target if so.
        /// </summary>
        void CheckForTargetReached()
        {
            float distance = Vector3.Distance(
                new Vector3(transform.localPosition.x, 0, transform.localPosition.z),
                new Vector3(currentTarget.localPosition.x, 0, currentTarget.localPosition.z)
            );

            if (distance < 0.1f)
            {
                ToggleTarget();
            }
        }

        /// <summary>
        /// Toggles the current target between targetA و targetB.
        /// </summary>
        void ToggleTarget()
        {
            movingToA = !movingToA;
            currentTarget = movingToA ? targetA : targetB;
        }
    }
}
