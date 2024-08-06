using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Piranest.UI
{
    public class NavigationButtons : MonoBehaviour
    {
        [Header("Target Image Settings")]
        public Image targetImage; // Image that will change its sprite.

        [Header("UI Buttons")]
        public Button gamesButton;
        public Button vendorsButton;
        public Button leaderboardButton;
        public Button profileButton;

        [Header("Sprites")]
        public Sprite gamesSprite; // Sprite for Games button
        public Sprite vendorsSprite; // Sprite for Vendors button
        public Sprite leaderboardSprite; // Sprite for LeaderBoard button
        public Sprite profileSprite; // Sprite for Profile button

        void Start()
        {
            // Initialize buttons with a transparent color
            SetButtonInitialColor(gamesButton);
            SetButtonInitialColor(vendorsButton);
            SetButtonInitialColor(leaderboardButton);
            SetButtonInitialColor(profileButton);

            // Add button click listeners using UnityAction
            gamesButton.onClick.AddListener(CreateChangeSpriteAction(gamesSprite));
            vendorsButton.onClick.AddListener(CreateChangeSpriteAction(vendorsSprite));
            leaderboardButton.onClick.AddListener(CreateChangeSpriteAction(leaderboardSprite));
            profileButton.onClick.AddListener(CreateChangeSpriteAction(profileSprite));
        }

        void SetButtonInitialColor(Button button)
        {
            Color transparentColor = new Color(0, 0, 0, 0);
            button.GetComponent<Image>().color = transparentColor;
        }

        // Returns a UnityAction that changes the sprite to the specified one
        UnityAction CreateChangeSpriteAction(Sprite newSprite)
        {
            return new UnityAction(() =>
            {
                targetImage.sprite = newSprite;
            });
        }
    }
}