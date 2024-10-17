using Piranest.AR;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.AR
{
    /// <summary>
    /// Handles the instantiation and management of prefabs attached to a moving object.
    /// </summary>
    public class AttachPrefab : MonoBehaviour
    {
        [Header("Prefab Settings")]
        [Tooltip("List of prefabs to be instantiated as children")]
        [SerializeField] private List<GameObject> prefabList = new List<GameObject>();

        [Tooltip("The moving object to which the prefabs will be attached")]
        [SerializeField] private Transform movingObject;

        [Header("Game Settings")]
        [Tooltip("Enable test mode for immediate prefab instantiation")]
        [SerializeField] private bool isTest = true;

        [HideInInspector] public bool firstPuzzle = true;

        public GameObject lastInstantiatedObject; // Stores the last instantiated object
        private int instantiateCount = 0; // Tracks the number of instantiated prefabs

        // Cached references to dependent components
        private ObjectMover objectMover;
        private PanelObject panelObject;
        private TimerDisplay timerDisplay;

        /// <summary>
        /// Initializes the script by caching references and handling initial prefab instantiation or panel setup.
        /// </summary>
        private void Start()
        {
            // Cache reference to ObjectMover
            objectMover = FindObjectOfType<ObjectMover>();

            if (isTest)
            {
                // In test mode, cache TimerDisplay and instantiate a prefab immediately
                timerDisplay = FindObjectOfType<TimerDisplay>();
                if (timerDisplay == null)
                {
                    Debug.LogError("TimerDisplay not found in the scene.");
                    enabled = false;
                    return;
                }

                lastInstantiatedObject = null;
                InstantiatePrefab();
            }
            else
            {
                // In non-test mode, cache PanelObject and begin the panel sequence
                panelObject = FindObjectOfType<PanelObject>();
                if (panelObject == null)
                {
                    Debug.LogError("PanelObject not found in the scene.");
                    enabled = false;
                    return;
                }

                panelObject.Begin();
            }
        }

        /// <summary>
        /// Updates each frame to check for user input to drop the last instantiated prefab.
        /// </summary>
        private void Update()
        {
            
        }

        /// <summary>
        /// Starts the game by resetting the instantiated object, starting the timer, and instantiating a new prefab.
        /// </summary>
        public void StartGame()
        {
            lastInstantiatedObject = null;

            if (timerDisplay == null)
            {
                timerDisplay = FindObjectOfType<TimerDisplay>();
                if (timerDisplay == null)
                {
                    Debug.LogError("TimerDisplay not found in the scene.");
                    return;
                }
            }

            timerDisplay.TimerStart();
            InstantiatePrefab();
        }

        /// <summary>
        /// Drops the last instantiated object by enabling its gravity and re-parenting it.
        /// </summary>
        public void DropObj()
        {
            if (lastInstantiatedObject != null)
            {
                DropObject();
            }
            else
            {
                Debug.LogWarning("No instantiated object to drop.");
            }
        }

        /// <summary>
        /// Activates gravity and changes the parent of the last instantiated object.
        /// </summary>
        private void DropObject()
        {
            Rigidbody rb = lastInstantiatedObject.AddComponent<Rigidbody>();
            rb.mass = 1;
            rb.drag = 2;
            rb.angularDrag = 0;
            rb.freezeRotation = true;
            if (rb != null)
            {
                rb.useGravity = true;
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found on the instantiated object.");
            }

            // Re-parent the instantiated object to this GameObject's parent, if available
            if (transform.parent != null)
            {
                lastInstantiatedObject.transform.SetParent(transform.parent);
            }
            else
            {
                Debug.LogWarning("This GameObject has no parent to reassign the instantiated object.");
            }
        }

        /// <summary>
        /// Instantiates a random prefab from the list and attaches it to the moving object.
        /// </summary>
        public void InstantiatePrefab()
        {
            if (prefabList.Count == 0)
            {
                Debug.LogWarning("Prefab list is empty. Cannot instantiate any prefabs.");
                return;
            }

            if (movingObject == null)
            {
                Debug.LogError("MovingObject is not assigned. Please assign it in the Inspector.");
                return;
            }

            // Select a random prefab from the list
            int index = Random.Range(0, prefabList.Count);
            GameObject selectedPrefab = prefabList[index];

            // Instantiate the prefab as a child of the moving object
            lastInstantiatedObject = Instantiate(selectedPrefab, Vector3.zero, movingObject.rotation);
            lastInstantiatedObject.transform.SetParent(transform.parent);
            instantiateCount++;
            //objectMover.IncreaseHeight(instantiateCount);
        }
    }
}
