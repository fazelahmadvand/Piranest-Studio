using Piranest.Model;
using Piranest.UI.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class GameFinishView : View
    {

        [SerializeField] private Button seeAllVendorsBtn, backToGameBtn;

        [SerializeField] private TMP_Text gemTxt, extraGemTxt;

        [Space]
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorParent;
        [SerializeField] private VendorInfoView vendorInfoView;
        [SerializeField] private ItemData itemData;
        [SerializeField] private HeaderView headerView;

        public void UpdateFinish(Game game, int timeBonusGem, int prize, Action OnSeeAllVendor, Action BackToGame)
        {
            Show();
            gemTxt.text = prize.ToString();
            extraGemTxt.text = $"{timeBonusGem}";
            seeAllVendorsBtn.SetEvent(OnSeeAllVendor);
            backToGameBtn.SetEvent(BackToGame);
            CreateVendorsOfGameCity(game);
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
