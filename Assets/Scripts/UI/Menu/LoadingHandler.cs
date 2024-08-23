using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Piranest
{
    public class LoadingHandler : Singleton<LoadingHandler>
    {
        [SerializeField] protected GameObject root;
        [SerializeField] private TMP_Text txt;

        [SerializeField] private List<GameObject> bars;
        [SerializeField] private float duration = .25f;
        [SerializeField] private float scaleSize = -.5f;

        public void Show()
        {
            StartCoroutine(ShowAnimation());
            root.SetActive(true);
        }

        public void Hide()
        {
            root.SetActive(false);
        }

        public void UpdateText(string value)
        {
            txt.text = value;
        }
        private IEnumerator ShowAnimation()
        {
            foreach (var bar in bars)
            {
                bar.transform.DOPunchScale(Vector3.one * scaleSize, duration);
                yield return new WaitForSeconds(duration);
            }

            StartCoroutine(ShowAnimation());
        }

    }
}
