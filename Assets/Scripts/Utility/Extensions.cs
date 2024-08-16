using System;
using UnityEngine;
using UnityEngine.UI;
namespace Piranest
{
    public static class Extensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            int count = transform.childCount;
            for (int i = count - 1; i > -1; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }


        public static void AddEvent(this Button btn, Action OnClick)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnClick?.Invoke());
        }


    }
}
