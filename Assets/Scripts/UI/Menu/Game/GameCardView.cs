using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class GameCardView : View
    {
        [SerializeField] private Image img;
        [SerializeField] private Button btn;
        [SerializeField] private TMP_Text locationTxt, rewardTxt, nameTxt;

        [SerializeField] private TextureSaveData textureSaveData;

        [SerializeField] private GameData gameData;

        public void UpdateCard(Game game, Action OnClick)
        {
            Show();
            img.sprite = textureSaveData.GetSprite(game.CoverImageUrl);
            btn.SetEvent(OnClick);

            locationTxt.text = game.City;
            rewardTxt.text = gameData.CalculateGameReward(game.Id).ToString();
            nameTxt.text = game.Name;
        }


        

    }
}
