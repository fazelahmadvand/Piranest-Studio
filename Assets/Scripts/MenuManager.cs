using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI
{
    public class MenuManager : Singleton<MenuManager>
    {

        [SerializeField] private List<View> views = new();

        private void Start()
        {

            foreach (var view in views)
            {
                view.InitView();
            }
        }



    }
}
