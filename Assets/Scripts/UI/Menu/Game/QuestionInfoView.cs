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

        [Header("Time")]
        [SerializeField] private GameObject timerRoot;
        [SerializeField] private TMP_Text timerTxt;

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
            TimerHandler.TimeUpdate += HandleTime;
            TimerHandler.TimeEnd += DisableTime;
        }

        private void OnDestroy()
        {
            TimerHandler.TimeUpdate -= HandleTime;
            TimerHandler.TimeEnd -= DisableTime;
        }

        public void UpdateInfo(GameChapterQuestion question, Action<QuestionStateType> OnSubmit)
        {
            HandleTime();
            Show();
            submitBtn.interactable = false;
            var questions = question.Options.Split(',').ToList();
            HandleAnswers(questions);
            rightAnswerIndex = questions.FindIndex(q => q.Equals(question.RightAnswer));
            questionImg.sprite = textureData.GetSprite(question.MediaUrl);
            descriptionTxt.text = question.Story;
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

        private void HandleTime()
        {
            if (TimerHandler.HasTime)
            {
                timerRoot.SetActive(true);
                string time = Utility.SecondToTimeString(TimerHandler.Timer);
                timerTxt.text = $"{time}";
            }
            else
            {
                DisableTime();
            }
        }

        private void DisableTime()
        {
            timerRoot.SetActive(false);
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
