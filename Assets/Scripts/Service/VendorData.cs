using DynamicPixels.GameService;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{

    [CreateAssetMenu(fileName = nameof(VendorData), menuName = Utility.SCRIPTABLE_PATH + nameof(VendorData))]
    public class VendorData : BaseServiceData
    {
        [field: SerializeField]
        public List<Vendor> Vendors { get; set; }


        private const string VendorTableId = "6550d76675e62b435ba7450c";
        public async void GetVendors()
        {
            var findParam = new FindParams()
            {
                tableId = VendorTableId,
            };

            try
            {
                var response = await ServiceHub.Table.Find<Vendor, FindParams>(findParam);
                Vendors = response.List;
            }
            catch (System.Exception)
            {

                throw;
            }


        }



    }
}
