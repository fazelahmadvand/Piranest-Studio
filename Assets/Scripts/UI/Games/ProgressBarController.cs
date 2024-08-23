using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Piranest.UI
{
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private GameObject imagePrefab; 
        [SerializeField] private Transform container; 
        [SerializeField] private int numberOfImages = 5; 
        [SerializeField] private float totalWidth = 100f;
        [SerializeField] private HorizontalLayoutGroup layoutGroup;
        [SerializeField] private bool testScript = false;
        void Start()
        {
            if (testScript)
            { GenerateImagesWithFixedSpacing(); }
        }

        public void GenerateImagesWithFixedSpacing()
        {
            float totalSpacing = (int)layoutGroup.spacing * (numberOfImages - 1); // Use existing spacing
            float availableWidth = totalWidth - totalSpacing; // Adjust available width for images
            float individualWidth = availableWidth / numberOfImages; // Calculate width for each image

            for (int i = 0; i < numberOfImages; i++)
            {
                GameObject img = Instantiate(imagePrefab, layoutGroup.transform);
                img.GetComponent<RectTransform>().sizeDelta = new Vector2(individualWidth, img.GetComponent<RectTransform>().sizeDelta.y);
                // Optionally, set the image sprite and other properties here
            }
        }
    }
}