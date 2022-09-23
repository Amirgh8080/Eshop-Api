using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.UserAgg
{
    public class UserAddress : BaseEntity
    {
        public UserAddress(string shire, string city, string postalCode, string postaAdderss, string phoneNumber, string name, string family, string nationalCode)
        {
            Shire = shire;
            City = city;
            PostalCode = postalCode;
            PostaAdderss = postaAdderss;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
        }

        public long UserId { get; internal set; }
        public string Shire { get; private set; }
        public string City { get; set; }
        public string PostalCode { get; private set; }
        public string PostaAdderss { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }


        public void Edit(string shire, string city, string postalCode, string postaAdderss, string phoneNumber, string name, string family, string nationalCode)
        {
            Shire = shire;
            City = city;
            PostalCode = postalCode;
            PostaAdderss = postaAdderss;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
        }

        public void Guard(string shire, string city, string postalCode, string postaAdderss, string phoneNumber, string name, string family, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shire, nameof(shire));
            NullOrEmptyDomainDataException.CheckString(city, nameof(city));
            NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
            NullOrEmptyDomainDataException.CheckString(postaAdderss, nameof(postaAdderss));
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            NullOrEmptyDomainDataException.CheckString(name, nameof(name));
            NullOrEmptyDomainDataException.CheckString(family, nameof(family));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
        }
    }
}
