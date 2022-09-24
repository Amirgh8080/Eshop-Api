using Common.Domain;

namespace Shop.Domain.OrderAgg
{
    public class OrderAddress : BaseEntity
    {
        public OrderAddress(string shire, string city, string postalCode, string postaAdderss,
            string phoneNumber, string name, string family, string nationalCode)
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

        public long OrderId { get;internal set; }
        public string Shire { get; private set; }
        public string City { get; set; }
        public string PostalCode { get; private set; }
        public string PostaAdderss { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public Order Order { get; set; }
    }
}
