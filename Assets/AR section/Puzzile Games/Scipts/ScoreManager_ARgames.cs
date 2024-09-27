using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics; // Required for BigInteger

namespace Piranest.AR
{
    public class ScoreManager_ARgames : MonoBehaviour
    {
        private BigInteger score = 0; // Using BigInteger to handle large scores
        [SerializeField] private Text scoreText;

        // Dictionary to memoize Fibonacci numbers
        private Dictionary<int, BigInteger> fibonacciCache = new Dictionary<int, BigInteger>();

        private void Start()
        {
            // Initialize the Fibonacci cache with the first two Fibonacci numbers
            fibonacciCache[1] = 1;
            fibonacciCache[2] = 1;
            UpdateScoreText();
        }

        /// <summary>
        /// Calculates the nth Fibonacci number using an iterative approach with memoization.
        /// </summary>
        /// <param name="n">The position in the Fibonacci sequence (1-based).</param>
        /// <returns>The nth Fibonacci number as a BigInteger.</returns>
        private BigInteger CalculateFibonacci(int n)
        {
            if (n <= 0)
            {
                Debug.LogError("Fibonacci number is not defined for n <= 0.");
                return 0;
            }

            if (fibonacciCache.ContainsKey(n))
            {
                return fibonacciCache[n];
            }

            // Start from the highest cached Fibonacci number
            int start = fibonacciCache.Count;
            if (n <= start)
            {
                return fibonacciCache[n];
            }

            BigInteger a = fibonacciCache[start - 1 > 0 ? start - 1 : 1];
            BigInteger b = fibonacciCache[start];

            for (int i = start + 1; i <= n; i++)
            {
                BigInteger fib = a + b;
                fibonacciCache[i] = fib;
                a = b;
                b = fib;
            }

            return fibonacciCache[n];
        }

        /// <summary>
        /// Changes the score by adding the Fibonacci number corresponding to the input x.
        /// </summary>
        /// <param name="x">The input number to map to the Fibonacci sequence.</param>
        public void ChangeScore(int x)
        {
            BigInteger fibNumber = CalculateFibonacci(x);
            score += fibNumber;
            UpdateScoreText();
        }

        /// <summary>
        /// Resets the score and clears the Fibonacci cache.
        /// </summary>
        public void Lose()
        {
            UpdateScoreText();
            score = 0;
            fibonacciCache.Clear();
            // Reinitialize the Fibonacci cache
            fibonacciCache[1] = 1;
            fibonacciCache[2] = 1;
        }

        /// <summary>
        /// Updates the score text in the UI.
        /// </summary>
        private void UpdateScoreText()
        {
            scoreText.text = "Your score: " + score.ToString();
        }

        /// <summary>
        /// Example usage: Change score when a button is clicked.
        /// Ensure that the input x is provided appropriately.
        /// </summary>
        /// <param name="x">The input number to map to the Fibonacci sequence.</param>
        public void OnChangeScoreButtonClicked(int x)
        {
            ChangeScore(x);
        }
    }
}
