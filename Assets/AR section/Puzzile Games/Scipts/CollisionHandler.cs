using UnityEngine;

namespace Piranest.AR
{
    public class CollisionHandler : MonoBehaviour
    {
        private bool isCollide = true, puzzlecollide = true, firstCollide = false;
        private GameObject perviousPuzzle;
        [SerializeField] private GameObject vfx_Particles;
        [HideInInspector] public int puzzleNum = 0;
        public enum PuzzleType { Red, Green, Blue };
        public PuzzleType typeP;
        private bool nextPuzzle = true;
        private bool isBeingDestroyed = false;
        private bool hasCollided = false; // Flag to track if a collision has already been handled

        private void Start()
        {
            transform.position = Vector3.zero;
        }
        private void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
                transform.position = parentScript.transform.position;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
            if (hasCollided) return; // Skip further processing if collision has already been handled
            float distanceY = parentScript.gameObject.transform.position.y - transform.position.y;
            if (distanceY < 0.1)
            {
                isCollide = false;
                Losing();
            }
            if (isCollide)
            {
                // Find the AttachPrefab script in the scene and call InstantiatePrefab
                if (collision.gameObject.tag == "Puzzle" && puzzlecollide)
                {
                    if (parentScript != null)
                    {
                        hasCollided = true; // Set the flag to prevent multiple collisions
                        ScoreManager_ARgames gm = FindObjectOfType<ScoreManager_ARgames>();
                        CollisionHandler collisionHandler = collision.gameObject.GetComponent<CollisionHandler>();
                        InstantiatePuzzle();
                        if (typeP == collisionHandler.typeP)
                        {
                            if (puzzleNum <= collisionHandler.puzzleNum)
                            {
                                puzzleNum = collisionHandler.puzzleNum + 1;
                                perviousPuzzle = collision.gameObject;
                                if (puzzleNum >= 2)
                                {
                                    gm.ChangeScore(+1);
                                    Debug.Log("new Score");
                                    DestroyPuzzle();
                                }
                            }
                        }

                    }
                    else
                    {
                        Debug.LogError("AttachPrefab script not found in the scene.");
                    }
                }
                else if (collision.gameObject.tag == "Plane")
                {
                    if (parentScript.firstPuzzle)
                    {
                        parentScript.firstPuzzle = false;
                        firstCollide = true;
                        isCollide = false;
                        InstantiatePuzzle();
                    }
                    else if (!firstCollide)
                    {
                        InstantiatePuzzle();
                    }
                }
                else if (collision.gameObject.tag == "OutSide")
                {
                    // Handle losing scenario
                    InstantiatePuzzle();

                }
            }
            else
            {
                if (collision.gameObject.tag == "Plane" && !firstCollide)
                {
                    // Handle losing scenario
                }
            }
        }

        private void InstantiatePuzzle()
        {
            if (nextPuzzle)
            {
                AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
                parentScript.InstantiatePrefab();
                nextPuzzle = false;
            }
        }

        public void DestroyPuzzle()
        {
            if (!isBeingDestroyed)
            {
                isBeingDestroyed = true;

                if (perviousPuzzle != null)
                {
                    perviousPuzzle.gameObject.GetComponent<CollisionHandler>().DestroyPuzzle();
                }
                Instantiate(vfx_Particles, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        public void Losing()
        {
            Debug.Log("You Lose!");
            AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
            parentScript.gameObject.SetActive(false);
            PanelObject panelObject = FindObjectOfType<PanelObject>();
            panelObject.Failed();
        }
    }
}
