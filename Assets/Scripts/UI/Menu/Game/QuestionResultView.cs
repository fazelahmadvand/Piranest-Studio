using Piranest.Model;
using Piranest.UI.Menu;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class QuestionResultView : View
    {
        [SerializeField] private TMP_Text gemTxt;

        [Space]
        [SerializeField] private TMP_Text resultTxt;
        [SerializeField] private Color successColor, failColor;
        [SerializeField] private string successTitle, failTitle;
        [SerializeField] private TMP_Text descriptionTxt;
        [Space]
        [SerializeField] private Button nextQuestionBtn;
        [SerializeField] private Image nextQuestionLocationImg;

        [Header("Vendors")]
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorParent;
        [SerializeField] private VendorInfoView vendorInfoView;
        [SerializeField] private ItemData itemData;
        [SerializeField] private HeaderView headerView;

        public void UpdateResult(Game game, bool isAnswerTrue, int gemPrize, GameChapterQuestion currentQuestion, Action OnClick)
        {
            Show();
            CreateVendorsOfGameCity(game);
            descriptionTxt.text = currentQuestion.Description;

            if (isAnswerTrue)
            {
                resultTxt.text = successTitle;
                resultTxt.color = successColor;
                gemTxt.text = gemPrize.ToString();
            }
            else
            {
                resultTxt.text = failTitle;
                resultTxt.color = failColor;
                gemTxt.text = 0.ToString();
            }
            nextQuestionBtn.SetEvent(OnClick);
        }

        private void CreateVendorsOfGameCity(Game game)
        {
            vendorParent.DestroyChildren();
            var vendors = itemData.GetVendorsByCity(game.City);
            foreach (var v in vendors)
            {
                var newCard = Instantiate(vendorCard, vendorParent);
                newCard.UpdateCard(v, () =>
                {
                    vendorInfoView.UpdateCard(v.Id);
                    Hide();
                    headerView.UpdateHeader("Continue Game", () =>
                    {
                        Show();
                        vendorInfoView.Hide();
                    });
                });
            }

        }

    }
}
