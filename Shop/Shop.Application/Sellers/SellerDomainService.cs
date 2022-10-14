using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers;

public class SellerDomainService:ISellerDomainService
{
    private readonly ISellerRepository _repository;

    public SellerDomainService(ISellerRepository repository)
    {
        _repository = repository;
    }
    public bool IsSellerInformationValid(Seller seller)
    {
        var sellerIsExists = _repository
            .Exists(s => s.UserId == seller.UserId || s.NationalCode == seller.NationalCode); 
        return !sellerIsExists;
    }

    public bool DoesNationalCodeExistInDataBase(string nationalCode)
    {
      return _repository
            .Exists(s =>  s.NationalCode == nationalCode);  
    }
}