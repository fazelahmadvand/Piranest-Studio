using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private AuthData authData;
        [SerializeField] private VendorData vendorData;

        public event Action OnGettingData;
        public event Action OnDataLoaded;

        private async void Start()
        {
            OnGettingData?.Invoke();
            await Task.Delay(1000);
            await vendorData.FillVendors();
            await Task.Delay(500);

            OnDataLoaded?.Invoke();

        }






    }
}
