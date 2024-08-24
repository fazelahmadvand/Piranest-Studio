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

        [SerializeField] private Sprite nonSelectedAnswerSprite, selectedAnswerSprite;
        [SerializeField] private List<ButtonView> answers = new();

        [SerializeField] private Button submitBtn;

        [Space]
        [SerializeField] private float changeColorDuration = .5f;
        [SerializeField] private Color rightAnswer, wrongAnswer;

        private int index;
        private int rightAnswerIndex;
        private bool isAnswered;

        public void UpdateInfo(GameChapterQuestion question, Action<int> OnSubmit)
        {
            Show();
            isAnswered = false;
            submitBtn.interactable = false;
            var questions = question.Options.Split(',').ToList();
            HandleAnswers(questions);
            rightAnswerIndex = questions.FindIndex(q => q.Equals(question.RightAnswer));

            descriptionTxt.text = question.Description;
            whichAnswerTxt.text = question.Question;

            submitBtn.AddEvent(() =>
            {
                if (isAnswered) return;
                isAnswered = true;
                StartCoroutine(Utility.DoAfter(changeColorDuration + .1f, ShowAnswer, () =>
                {
                    OnSubmit?.Invoke(index);
                }));
            });
        }

        private void HandleAnswers(List<string> questions)
        {
            for (int i = 0; i < answers.Count; i++)
            {
                var card = answers[i];
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


        private void ShowAnswer()
        {
            var card = answers[index];
            var color = index == rightAnswerIndex ? rightAnswer : wrongAnswer;

            var tween = card.ChangeColor(changeColorDuration / 2, color);
            tween.onComplete += () => { card.ChangeColor(changeColorDuration / 2, Color.white); };


        }

    }
}
