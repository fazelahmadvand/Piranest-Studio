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

        [SerializeField] private TMP_Text gemTxt;


        public override void InitView()
        {
            base.InitView();
            //backToGameBtn.SetEvent();

        }

        public void UpdateFinish(int prize, Action OnSeeAllVendor, Action BackToGame)
        {
            Show();
            gemTxt.text = prize.ToString();
            seeAllVendorsBtn.SetEvent(OnSeeAllVendor);
            backToGameBtn.SetEvent(BackToGame);

        }

    }
}
