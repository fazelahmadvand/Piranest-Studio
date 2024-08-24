using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Piranest.UI
{
    public class ChapterHeaderInfo : View
    {
        [SerializeField] private TMP_Text chapterTxt;
        [SerializeField] private Transform questionHolder;
        [SerializeField] private HeaderQuestionView questionPrefab;

        private List<HeaderQuestionView> questions = new();

        public void Create(ChapterInfo info)
        {
            chapterTxt.text = $"Chapter {info.chapterNumber}";
            questionHolder.DestroyChildren();
            for (int i = 0; i < info.questionStates.Count; i++)
            {
                var question = Instantiate(questionPrefab, questionHolder);

                question.UpdateCard(info.questionStates[i], $"Q{i + 1}");
                questions.Add(question);
            }

        }



    }
}
