using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public class LoadingHandler : Singleton<LoadingHandler>
    {
        [SerializeField] protected GameObject root;
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
