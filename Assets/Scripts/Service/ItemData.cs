using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = Utility.SCRIPTABLE_PATH + nameof(ItemData))]
    public class ItemData : BaseServiceData
    {
        [field: SerializeField]
        public List<Vendor> Vendors { get; private set; }
        [field: SerializeField]
        public List<VendorCoupon> VendorCoupons { get; private set; }

        public event Action OnLoadItems;


        private const string VEDNOR_TABLE_ID = "6550d76675e62b435ba7450c";
        private const string VENDOR_COUPON_TABLE_ID = "6558da665762ed93c7f44ee4";

        public async override Task Init()
        {
            await GetItems();
        }

        public Vendor GetVendor(int id)
        {
            return Vendors.FirstOrDefault(v => v.Id == id);
        }

        public List<VendorCoupon> GetVendorCoupon(int vendorId)
        {
            return VendorCoupons.Where(v => v.VendorId == vendorId).ToList();
        }

        public List<Vendor> GetVendorsByCity(string cityName)
        {
            return Vendors.Where(v => v.City.Equals(cityName)).ToList();
        }

        public async Task GetItems()
        {
            await FillVendors();
            await FillVendorCoupons();
            OnLoadItems?.Invoke();

        }

        public async Task FillVendorCoupons()
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = VENDOR_COUPON_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<VendorCoupon, FindParams>(findParam);
                VendorCoupons = response.List;
            }
            catch (DynamicPixelsException)
            {
                throw;
            }
        }

        public async Task FillVendors()
        {
            var findParam = new FindParams()
            {
                options = new(),
                tableId = VEDNOR_TABLE_ID,
            };

            try
            {
                var response = await ServiceHub.Table.Find<Vendor, FindParams>(findParam);
                Vendors = response.List;
            }
            catch (DynamicPixelsException)
            {
                throw;
            }
        }



    }
}
