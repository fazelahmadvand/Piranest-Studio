using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class LeaderboardCardView : View
    {

        [SerializeField] private TMP_Text rankTxt, nameTxt, gemAmountTxt;
        [SerializeField] private Image borderImg;
        [SerializeField] private Color selfColor, otherColor;


        public void UpdateCard(int rank, string name, int gem, bool isLocal)
        {
            rankTxt.text = rank.ToString();
            nameTxt.text = name;
            gemAmountTxt.text = gem.ToString();
            borderImg.color = isLocal ? selfColor : otherColor;
        }



    }
}
