using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class GameChapterView : View
    {
        [SerializeField] private Image chapterImg;
        [SerializeField] private TMP_Text descriptionTxt;
        [SerializeField] private Button startChapter;
        [SerializeField] private RawImage mapImg;
        [SerializeField] private Button btnMap;

        [SerializeField] private TextureSaveData saveData;

        public void UpdateChapter(GameChapter chapter, GameChapterQuestion first, GameChapterQuestion last, Action OnChapterStart)
        {
            Show();
            chapterImg.sprite = saveData.GetSprite(chapter.MediaUrl);
            descriptionTxt.text = chapter.Description;
            startChapter.SetEvent(OnChapterStart);
            DownloadMap(first, last);

            btnMap.SetEvent(() =>
            {
                //TODO
            });
        }


        private void DownloadMap(GameChapterQuestion first, GameChapterQuestion last)
        {
            var firstPoint = new Vector2 { x = first.LocationLat, y = first.LocationLong };
            var lastPoint = new Vector2 { x = last.LocationLat, y = last.LocationLong };
            int width = (int)mapImg.rectTransform.rect.width;
            int height = (int)mapImg.rectTransform.rect.height;
            string url = Utility.TwoPointsGoogleMap(firstPoint, lastPoint, width, height);

            StartCoroutine(API.API.DownloadTexture(url, (tex) =>
            {
                mapImg.texture = tex;
            }));


        }


    }
}
