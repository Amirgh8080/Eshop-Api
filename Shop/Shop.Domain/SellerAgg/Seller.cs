using Common.Domain;
using Common.Domain.Exceptions;
using MediatR;
using Shop.Domain.SellerAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg
{
    public class Seller:AggregateRoot
    {
 

        public long UserId { get;private set; }
        public string ShopName { get;private set; }
        public string NationalCode { get; private set; }
        public SellerStatus Status { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public List<SellerInventory> Inventories { get; private set; }

        public Seller(long userId, string shopName, string nationalCode,ISellerDomainService domainSevice)
        {
            Guard(shopName, nationalCode);
            
            UserId = userId;
            ShopName = shopName;
            NationalCode = nationalCode;
            Inventories = new List<SellerInventory>();

            if (domainSevice.IsSellerInformationValid(this) == false)
                throw new InvalidDomainDataException("اطلاعات نا معتبر است.");

        }

        private Seller()
        {

        }

        public void ChangeStatus(SellerStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }

        public void Edit(string shopName, string nationalCode,SellerStatus status, ISellerDomainService domainService)
        {
            if(nationalCode != NationalCode)
                if(domainService.DoesNationalCodeExistInDataBase(nationalCode))
                    throw new InvalidDomainDataException("کد ملی متعلق به شخص دیگری است.");
                
            Guard(shopName, nationalCode);
            ShopName = shopName;
            NationalCode = nationalCode;
            Status = status;
        }

        public void AddInventory(SellerInventory inventory)
        {
            if (Inventories.Any(i => i.ProductId == inventory.ProductId))
                throw new InvalidDomainDataException("این محصول قبلا ثبت شده است");

            Inventories.Add(inventory);
        }

        public void EditInventory(long inventoryId, int count, int price, int? discountPercentage)
        {
            var currentInventory = Inventories.FirstOrDefault(f => f.Id == inventoryId);
            if (currentInventory == null)
                throw new NullOrEmptyDomainDataException("محصول یافت نشد");

            //TODO Check Inventories
            currentInventory.Edit(count, price, discountPercentage);
        }

        public void DeleteInventory(long inventoryId)
        {
            var inventory = Inventories.FirstOrDefault(i => i.Id ==inventoryId);
            if (inventory == null)
                throw new NullOrEmptyDomainDataException("محصول یافت نشد");

            Inventories.Remove(inventory);
        }

        public void Guard(string shopName, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if (IranianNationalIdChecker.IsValid(nationalCode))
            {
                throw new InvalidDomainDataException("کد ملی نامعتبر است");
            }
        }

    }
}
