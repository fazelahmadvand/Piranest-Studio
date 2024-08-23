using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class ButtonView : View
    {
        [SerializeField] protected Button btn;
        [SerializeField] protected Image img;
        [SerializeField] protected TMP_Text txt;
        [SerializeField] protected GameObject borderObj;

        public void UpdateButton(string name, Action OnClick)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
            txt.text = name;
        }

        public void UpdateButton(Action OnClick)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
        }

        public void SetImage(Sprite sprite)
        {
            img.sprite = sprite;
        }

        public void HandleBorder(bool isActive)
        {
            borderObj.SetActive(isActive);
        }


    }
}
