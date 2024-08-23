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

        [SerializeField] private TextureSaveData saveData;

        public void UpdateChapter(GameChapter chapter, Action OnChapterStart)
        {
            Show();
            chapterImg.sprite = saveData.GetSprite(chapter.MediaUrl);
            descriptionTxt.text = chapter.Description;
            startChapter.AddEvent(OnChapterStart);

        }


    }
}
