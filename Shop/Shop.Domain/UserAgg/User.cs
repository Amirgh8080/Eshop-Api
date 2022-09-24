using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class User : AggregateRoot
    {
        public User(string name, string family, string phoneNumber, string email, string password,
            Gender gender, IDomainUserService domainUser)
        {
            Guard(phoneNumber, email, domainUser);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> Addresses { get; private set; }



        public void Edit(string name, string family, string phoneNumber, string email,
            Gender gender, IDomainUserService domainUser)
        {
            Guard(phoneNumber, email, domainUser);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public static User RegisterUser(string email,string phoneNumber,string password,IDomainUserService domainUserService)
        {
            return new User("","",phoneNumber,email,password,Gender.None,domainUserService);
        }

        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }
        public void DeleteAddress(long addressId)
        {
            var address = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");


            Addresses.Remove(address);
        }

        public void EditAddress(UserAddress address)
        {
            var oldAdress = Addresses.FirstOrDefault(a=>a.Id==address.Id);
            if (oldAdress == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");


            Addresses.Remove(oldAdress);
            Addresses.Add(address);
        }

        public void ChargeWallet(Wallet wallet)
        {
            Wallets.Add(wallet);
        }

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(r => r.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }


        public void Guard(string phoneNumber, string email,IDomainUserService domainUser)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

            NullOrEmptyDomainDataException.CheckString(email,nameof(email));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");
            if (email.IsValidEmail() == false)
                throw new InvalidDomainDataException("ایمیل نامعتبر است");

           if(phoneNumber!= PhoneNumber)
                if(domainUser.IsPhoneNumberExist(phoneNumber))
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");

            if (email != Email)
                if (domainUser.IsEmailExist(email))
                    throw new InvalidDomainDataException("ایمیل تکراری است");


        }
    }
}
