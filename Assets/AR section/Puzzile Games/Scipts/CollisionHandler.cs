using UnityEngine;

namespace Piranest.AR 
{
    public class CollisionHandler : MonoBehaviour
    {
        private bool isCollide = true,puzzlecollide=true, firstCollide = false;

        void OnCollisionEnter(Collision collision)
        {
            if (isCollide)
            {
                // Find the AttachPrefab script in the scene and call InstantiatePrefab
                AttachPrefab parentScript = FindObjectOfType<AttachPrefab>();
                if (collision.gameObject.tag == "Puzzle" && puzzlecollide)
                {
                    if (parentScript != null)
                    {
                        puzzlecollide = false;
                        ScoreManager_ARgames gm = FindObjectOfType<ScoreManager_ARgames>();
                        gm.ChangeScore(+1);
                        parentScript.InstantiatePrefab();
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
                        parentScript.InstantiatePrefab();
                    }
                    else if(!firstCollide)
                    {
                        Losing();
                    }
                }
                /*else if (collision.gameObject.tag == "OutSide") 
                {
                    Losing();
                }*/
            }
            else
            {
                if (collision.gameObject.tag == "Plane" && !firstCollide)
                {
                    Losing();
                }
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