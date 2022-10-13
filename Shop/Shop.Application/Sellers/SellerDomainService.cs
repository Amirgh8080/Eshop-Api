using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers;

public class SellerDomainService:ISellerDomainService
{
    public bool IsSellerInformationValid(Seller seller)
    {
        throw new NotImplementedException();
    }

    public bool DoesNationalCodeExistInDataBase(string nationalCode)
    {
        throw new NotImplementedException();
    }
}