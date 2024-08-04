using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public class View : MonoBehaviour
    {
        [SerializeField] protected GameObject root;

        public virtual void InitView()
        {
            Hide();
        }

        public virtual void Show()
        {
            if (root != null)
            {
                root.SetActive(true);
            }
        }

        public virtual void Hide()
        {
            if (root != null)
            {
                root.SetActive(false);
            }
        }

    }
}
