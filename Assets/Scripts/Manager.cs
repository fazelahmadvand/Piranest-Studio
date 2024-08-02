using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private AuthData authData;
        [SerializeField] private VendorData vendorData;

        public event Action OnInitializing;
        public event Action OnInitialized;

        private async void Start()
        {
            OnInitializing?.Invoke();
            await Task.Delay(1000);
            await vendorData.FillVendors();


        }






    }
}
