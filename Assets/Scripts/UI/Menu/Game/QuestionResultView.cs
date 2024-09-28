using Piranest.HTTP;
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
        [SerializeField] private Image asnwerImg;
        [SerializeField] private Button nextQuestionBtn;

        [Space]
        [SerializeField] private GameObject nextQuestionTitle;
        [SerializeField] private Button nextQuestionLocationbtn;
        [SerializeField] private RawImage nextQuestionLocationRawImg;
        [Space]
        [SerializeField] private GameData gameData;
        [Header("Vendors")]
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorParent;
        [SerializeField] private VendorInfoView vendorInfoView;
        [SerializeField] private HeaderView headerView;
        [SerializeField] private ItemData itemData;
        [SerializeField] private TextureSaveData textureSaveData;


        public void UpdateResult(Game game, bool isAnswerTrue, int gemPrize, GameChapterQuestion currentQuestion, GameChapterQuestion nextQuestion, Action OnClick)
        {
            Show();
            CreateVendorsOfGameCity(game);
            descriptionTxt.text = currentQuestion.Description;
            HandleNextQuestionLocation(nextQuestion);
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
            var sprite = textureSaveData.GetSprite(currentQuestion.AnswerMediaUrl);
            asnwerImg.gameObject.SetActive(sprite != null);
            asnwerImg.sprite = sprite;
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

        private void HandleNextQuestionLocation(GameChapterQuestion next)
        {
            if (next == null)
            {
                nextQuestionLocationbtn.gameObject.SetActive(false);
                nextQuestionTitle.SetActive(false);
                nextQuestionLocationRawImg.gameObject.SetActive(false);
            }
            else
            {
                nextQuestionLocationbtn.gameObject.SetActive(true);
                nextQuestionLocationRawImg.gameObject.SetActive(true);
                nextQuestionTitle.SetActive(true);

                nextQuestionLocationbtn.SetEvent(() =>
                {
                    Utility.OpenGoogleMap(next.LocationLat, next.LocationLong);
                });
                var rect = nextQuestionLocationRawImg.rectTransform.rect;
                string googleMapUrl = Utility.OnePointGoogleMap(next.LocationLat, next.LocationLong, (int)rect.width, (int)rect.height);
                StartCoroutine(API.DownloadTexture(googleMapUrl, (tex) =>
                {
                    nextQuestionLocationRawImg.texture = tex;
                }, () =>
                {

                }));
            }

        }

    }
}
