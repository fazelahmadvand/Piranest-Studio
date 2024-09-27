using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.AR
{
    public class ScoreManager_ARgames : MonoBehaviour
    {
        private int score = 0;
        [SerializeField] private Text scoreText;
        public void ChangeScore(int x)
        {
            score += x;
            scoreText.text = "Your score : " + score;
        }
        //
        public void Lose() 
        {
            score = 0;
        }
    }
}