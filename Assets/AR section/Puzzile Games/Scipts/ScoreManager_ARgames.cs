using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using System.Threading.Tasks; // Required for BigInteger
using TMPro;

namespace Piranest.AR
{
    public class ScoreManager_ARgames : MonoBehaviour
    {
        private BigInteger score = 0; // Using BigInteger to handle large scores
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private AuthData authData;

        // Variable to keep track of the current Fibonacci index
        private int currentFibonacciIndex = -1;

        // Dictionary to store calculated Fibonacci numbers for performance optimization
        private Dictionary<int, BigInteger> fibonacciCache = new Dictionary<int, BigInteger>();

        private void Start()
        {
            // Initialize the dictionary with the first two Fibonacci numbers
            fibonacciCache[1] = 1;
            fibonacciCache[2] = 1;
            UpdateScoreText();
        }

        /// <summary>
        /// Calculates the nth Fibonacci number using an iterative method with memoization.
        /// </summary>
        /// <param name="n">The position in the Fibonacci sequence (1-based).</param>
        /// <returns>The nth Fibonacci number as a BigInteger.</returns>
        private BigInteger CalculateFibonacci(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            if (fibonacciCache.ContainsKey(n))
            {
                return fibonacciCache[n];
            }

            // Find the highest cached Fibonacci index
            int lastCachedIndex = 0;
            foreach (var key in fibonacciCache.Keys)
            {
                if (key > lastCachedIndex)
                    lastCachedIndex = key;
            }

            BigInteger a = fibonacciCache.ContainsKey(lastCachedIndex - 1) ? fibonacciCache[lastCachedIndex - 1] : 1;
            BigInteger b = fibonacciCache[lastCachedIndex];

            // Calculate Fibonacci numbers from the last cached index + 1 up to n
            for (int i = lastCachedIndex + 1; i <= n; i++)
            {
                BigInteger fib = a + b;
                fibonacciCache[i] = fib;
                a = b;
                b = fib;
            }

            return fibonacciCache[n];
        }

        /// <summary>
        /// Adds score based on the current Fibonacci number.
        /// Each call adds the next Fibonacci number in the sequence to the score.
        /// </summary>
        public void AddFibonacciScore()
        {
            BigInteger fibNumber = CalculateFibonacci(currentFibonacciIndex);
            score = fibNumber;
            UpdateScoreText();

            Debug.Log($"Added F({currentFibonacciIndex}) = {fibNumber} to score.");
        }

        /// <summary>
        /// Changes the score by adding a given value.
        /// This method is kept for compatibility with the original code.
        /// </summary>
        /// <param name="x">The value to add to the score.</param>
        public void ChangeScore(int x)
        {
            currentFibonacciIndex += x;
            AddFibonacciScore();
        }

        /// <summary>
        /// Resets the score and the Fibonacci index.
        /// </summary>
        public async Task Lose()
        {
            currentFibonacciIndex = -1;
            fibonacciCache[1] = 1;
            fibonacciCache[2] = 1;
            UpdateScoreText();
            await authData.UpdateAccount((int)score);

            score = 0;
            fibonacciCache.Clear();
            
        }
        

        /// <summary>
        /// Updates the score display in the UI.
        /// </summary>
        private void UpdateScoreText()
        {
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Example usage: Adds a Fibonacci-based score when a button is clicked.
        /// Ensure that the button is connected to this method in the Inspector.
        /// </summary>
        public void OnAddFibonacciScoreButtonClicked()
        {
            AddFibonacciScore();
        }
    }
}