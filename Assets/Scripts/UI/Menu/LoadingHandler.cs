using UnityEngine;

namespace Piranest
{
    public class LoadingHandler : Singleton<LoadingHandler>
    {
        [SerializeField] protected GameObject root;
        public void Show()
        {
            root.SetActive(true);
        }

        public void Hide()
        {
            root.SetActive(false);

        }

    }
}
