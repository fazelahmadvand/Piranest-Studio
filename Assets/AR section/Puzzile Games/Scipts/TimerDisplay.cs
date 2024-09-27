using UnityEngine;
using TMPro; // Add the TextMeshPro namespace
using UnityEngine.UI;

namespace Piranest.AR
{
    public class TimerDisplay : MonoBehaviour
    {
        public Text timerText; // TMP_Text component to display the time
        public int durationInSeconds; // Duration of the timer in seconds

        private float remainingTime; // Remaining time
        private bool timerOn = false;
        void Start()
        {
            // Initialize the timer with the specified duration
            remainingTime = durationInSeconds;
        }

        void Update()
        {
            if (timerOn)
            {
                if (remainingTime > 0)
                {
                    // Decrease the remaining time by the elapsed time
                    remainingTime -= Time.deltaTime;

                    // Calculate minutes and seconds
                    int minutes = Mathf.FloorToInt(remainingTime / 60);
                    int seconds = Mathf.FloorToInt(remainingTime % 60);

                    // Display the remaining time in minute:second format
                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
                else
                {
                    // Display the end time message
                    timerText.text = "00:00";
                    PanelObject panelObject = FindObjectOfType<PanelObject>();
                    panelObject.Failed();
                }
            }
            else 
            {
                timerText.text = "";
            }
        }

        // Method to add time to the timer
        public void AddTime(int secondsToAdd)
        {
            remainingTime += secondsToAdd;
            // Update the display immediately to reflect the added time
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        //
        public void TimerStart() 
        {
            remainingTime = durationInSeconds;
            timerOn = true;
        }
        public void TimerOn() 
        {
            timerOn = true;
        }
        public void TimerOff() 
        {
            timerOn = false;
        }
    }
}