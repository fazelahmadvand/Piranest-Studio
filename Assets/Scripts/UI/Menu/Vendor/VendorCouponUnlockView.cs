using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class VendorCouponUnlockView : View
    {
        [SerializeField] private TMP_Text codeTxt;
        [SerializeField] private Button closeBtn;


        public override void InitView()
        {
            base.InitView();
            closeBtn.SetEvent(Hide);
        }


        public void UpdateCard(string code)
        {
            Show();
            codeTxt.text = code;
        }

    }
}
