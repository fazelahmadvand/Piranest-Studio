using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class HeaderQuestionView : View
    {
        [SerializeField] private Image img;
        [SerializeField] private TMP_Text txt;

        [SerializeField] private List<Map<QuestionStateType, Color>> colors = new();


        public void UpdateCard(QuestionStateType state, string val)
        {
            img.color = colors.FirstOrDefault(c => c.Key == state).Value;
            txt.text = val;
        }

    }
}
