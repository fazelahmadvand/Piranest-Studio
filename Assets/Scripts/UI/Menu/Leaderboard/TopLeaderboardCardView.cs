using TMPro;
using UnityEngine;

namespace Piranest
{
    public class TopLeaderboardCardView : View
    {
        [SerializeField] private TMP_Text nameTxt, gemCountTxt;

        public void UpdateCard(string name, int gemCount)
        {
            nameTxt.text = name;
            gemCountTxt.text = gemCount.ToString();
        }


    }
}
