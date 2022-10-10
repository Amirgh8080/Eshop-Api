namespace Shop.Domain.SellerAgg.Services;

public interface ISellerDomainService
{
    bool IsSellerInformationValid(Seller seller);
    bool DoesNationalCodeExistInDataBase(string nationalCode);
}

