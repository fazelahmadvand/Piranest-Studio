using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private AuthData authData;
        [SerializeField] private VendorData vendorData;
        [SerializeField] private SaveData saveData;


        [SerializeField] private List<View> views;

        public event Action OnGettingData;
        public event Action OnDataLoaded;

        private void Awake()
        {
            saveData.Init();
        }

        private async void Start()
        {

            foreach (var view in views)
            {
                view.InitView();
            }

            OnGettingData?.Invoke();
            await Task.Delay(1000);
            await vendorData.FillVendors();
            await Task.Delay(500);

            OnDataLoaded?.Invoke();



        }






    }
}
