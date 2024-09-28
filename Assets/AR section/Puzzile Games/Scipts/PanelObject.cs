using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.AR
{
    public class PanelObject : MonoBehaviour
    {
        public GameObject panel, playButton, generateButton , failedMenu;
        public void Begin() 
        {
            panel.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
        public void ClearEnviro() 
        {
            GameObject[] enviro = GameObject.FindGameObjectsWithTag("Plane");
            foreach (GameObject go in enviro)
            {
                Destroy(go.gameObject);
            }
        }
        public void PlayGame() 
        {
            AttachPrefab attachPrefab = FindObjectOfType<AttachPrefab>();
            attachPrefab.StartGame();
            playButton.gameObject.SetActive(false);
            SpawningObjectDetails spawningObjectDetails = FindObjectOfType<SpawningObjectDetails>();
            spawningObjectDetails.enabled = false;

            generateButton.gameObject.SetActive(true);
        }
        //
        public void NextPuzzle() 
        {
            AttachPrefab attachPrefab = FindObjectOfType<AttachPrefab>();
            attachPrefab.DropObj();
        }
        //
        public void Failed() 
        {
            generateButton.gameObject.SetActive(false);
            failedMenu.gameObject.SetActive(true); 
            TimerDisplay timerDisplay = FindObjectOfType<TimerDisplay>();
            timerDisplay.TimerOff();
            ScoreManager_ARgames gm = FindObjectOfType <ScoreManager_ARgames>();
            gm.Lose();
        }
        public void PlayAgain() 
        {
            failedMenu.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            ScoreManager_ARgames gm = FindObjectOfType<ScoreManager_ARgames>();
            //gm.ChangeScore(+1);
        }
    }
}