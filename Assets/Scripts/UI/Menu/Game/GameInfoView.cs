using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class GameInfoView : View
    {
        [SerializeField] private TMP_Text gameNameTxt, cityNameTxt, timeTxt, chapterCountTxt, storyTxt;
        [SerializeField] private Image gameImg;
        [SerializeField] private Button beginBtn, startLocationBtn, endLocationBtn;
        [SerializeField] private TMP_Text beginTxt;
        [SerializeField] private TextureSaveData textureSave;
        [SerializeField] private GameData gameData;

        public override void InitView()
        {

        }

        public void UpdateInfo(Game game, Action OnBegin)
        {
            Show();
            gameNameTxt.text = game.Name;
            cityNameTxt.text = game.City;
            timeTxt.text = $"{game.TimeLimit} sec";
            gameImg.sprite = textureSave.GetSprite(game.CoverImageUrl);
            var chapters = gameData.GetChapters(game.Id);
            storyTxt.text = game.Story;
            chapterCountTxt.text = chapters.Count.ToString();
            startLocationBtn.SetEvent(() =>
            {
                var firstChapter = chapters[0];
                var questions = gameData.GetQuestions(firstChapter.Id);
                var first = questions[0];
                Utility.OpenGoogleMap(first.LocationLat, first.LocationLong);
            });

            endLocationBtn.SetEvent(() =>
            {
                var lastChapter = chapters[^1];
                var questions = gameData.GetQuestions(lastChapter.Id);
                var last = questions[^1];
                Utility.OpenGoogleMap(last.LocationLat, last.LocationLong);
            });

            beginBtn.SetEvent(OnBegin);

        }


        public void HandleBeginButton(bool interactable, string val)
        {
            beginBtn.interactable = interactable;
            beginTxt.text = val;
        }



    }
}
