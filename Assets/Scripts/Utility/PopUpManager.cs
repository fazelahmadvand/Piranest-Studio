using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class PopUpManager : Singleton<PopUpManager>
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Transform popUpRoot;
        [SerializeField] private Button okBtn;
        [SerializeField] private TMP_Text infoTxt;
        [SerializeField] private TMP_Text okTxt;

        [SerializeField] private float duration = .5f;

        private void Awake()
        {
            //SelfSinglton();
        }

        public void Show(string info, Action OnOkClick, string btnTxt = "ok")
        {
            popUpRoot.localScale = Vector3.zero;
            popUpRoot.DOScale(Vector3.one, duration);
            root.SetActive(true);
            infoTxt.text = info;
            okTxt.text = btnTxt;
            Action Click = () =>
            {
                Hide();
            };
            if (OnOkClick != null)
                Click += OnOkClick;

            okBtn.SetEvent(Click);

        }


        public void Hide()
        {
            root.SetActive(false);
        }


    }
}
