using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class QuestionInfoView : View
    {
        [SerializeField] private TMP_Text whichAnswerTxt, descriptionTxt;

        [SerializeField] private List<ButtonView> answers = new();
        [SerializeField] private Image questionImg;
        [SerializeField] private Button submitBtn;
        [SerializeField] private Sprite nonSelectedAnswerSprite, selectedAnswerSprite;

        [Header("Header")]
        [SerializeField] private Transform chapterHeaderHolder;
        [SerializeField] private ChapterHeaderInfo chapterInfo;
        [SerializeField] private GameData gameData;
        [SerializeField] private TextureSaveData textureData;


        private int index;
        private int rightAnswerIndex;

        public override void InitView()
        {
            base.InitView();

        }

        public void UpdateInfo(GameChapterQuestion question, Action<QuestionStateType> OnSubmit)
        {
            Show();
            submitBtn.interactable = false;
            var questions = question.Options.Split(',').ToList();
            HandleAnswers(questions);
            rightAnswerIndex = questions.FindIndex(q => q.Equals(question.RightAnswer));
            questionImg.sprite = textureData.GetSprite(question.MediaUrl);
            descriptionTxt.text = question.Description;
            whichAnswerTxt.text = question.Question;

            submitBtn.SetEvent(() =>
            {
                OnSubmit?.Invoke(IsRightAnswer() ? QuestionStateType.Right : QuestionStateType.Wrong);
            });
        }

        private void HandleAnswers(List<string> questions)
        {
            foreach (var item in answers)
            {
                item.Hide();
            }
            Debug.Log("Q: "+questions.Count + " A:" + answers.Count);
            for (int i = 0; i < questions.Count; i++)
            {
                var card = answers[i];
                card.Show();
                var ii = i;
                var question = questions[i];
                card.UpdateText(question);
                card.SetImage(nonSelectedAnswerSprite);
                card.UpdateButton(() =>
                {
                    index = ii;
                    submitBtn.interactable = true;
                    foreach (var card in answers)
                    {
                        card.SetImage(nonSelectedAnswerSprite);
                    }
                    card.SetImage(selectedAnswerSprite);
                });
            }
        }


        public bool IsRightAnswer()
        {
            return index == rightAnswerIndex;
        }

        public void HandleChapterHeader(GameState state)
        {
            chapterHeaderHolder.DestroyChildren();

            foreach (var info in state.chaptersInfo)
            {
                var newC = Instantiate(chapterInfo, chapterHeaderHolder);
                newC.Create(info);
            }

        }

    }
}
