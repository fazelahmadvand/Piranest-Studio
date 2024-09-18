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

        public void Show(string info, Action OnOkClick, string btnTxt = "ok")
        {
            popUpRoot.localScale = Vector3.zero;
            popUpRoot.DOScale(Vector3.one, duration);
            root.SetActive(true);
            infoTxt.text = info;
            okTxt.text = btnTxt;
            OnOkClick += Hide;
            okBtn.SetEvent(OnOkClick);

        }


        public void Hide()
        {
            root.SetActive(false);
        }


    }
}
