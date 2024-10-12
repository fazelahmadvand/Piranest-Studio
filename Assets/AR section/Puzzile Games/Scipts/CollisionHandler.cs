using UnityEngine;

namespace Piranest.AR
{
    public class CollisionHandler : MonoBehaviour
    {
        private bool isCollide = true, puzzlecollide = true, firstCollide = false;
        private GameObject perviousPuzzle;
        [HideInInspector] public int puzzleNum = 0;
        public enum PuzzleType { Red, Green, Blue };
        public PuzzleType typeP;
        private bool nextPuzzle = true;
        private bool isBeingDestroyed = false;
        private bool hasCollided = false; // Flag to track if a collision has already been handled

        void OnCollisionEnter(Collision collision)
        {
            if (hasCollided) return; // Skip further processing if collision has already been handled

            if (transform.position.y > 0.75)
            {
                isCollide = false;
                Losing();
            }
            if (isCollide)
            {
                // Find the AttachPrefab script in the scene and call InstantiatePrefab
                AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
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
