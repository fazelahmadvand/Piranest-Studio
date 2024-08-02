using DynamicPixels.GameService;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{

    [CreateAssetMenu(fileName = nameof(VendorData), menuName = Utility.SCRIPTABLE_PATH + nameof(VendorData))]
    public class VendorData : BaseServiceData
    {
        [field: SerializeField]
        public List<Vendor> Vendors { get; set; }
        public event Action<List<Vendor>> OnLoadVendors;

        private const string VendorTableId = "6550d76675e62b435ba7450c";
        public async Task FillVendors()
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = VendorTableId,
            };

            try
            {
                var response = await ServiceHub.Table.Find<Vendor, FindParams>(findParam);
                Vendors = response.List;
                OnLoadVendors?.Invoke(Vendors);
            }
            catch (System.Exception)
            {

                throw;
            }


        }



    }
}
