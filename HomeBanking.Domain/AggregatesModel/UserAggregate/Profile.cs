using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Text.RegularExpressions;

namespace HomeBanking.Domain.AggregatesModel.UserAggregate
{
    public class Profile
      : Entity
    {
        private const string phoneMask = @"(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";

        private int? _userId;
        private string _name;
        private DateTime _birthday;
        private string _phoneNumber;
        private User _user;

        public string PhoneNumber { get => _phoneNumber; }
        public DateTime Birthday { get => _birthday; }
        public string Name { get => _name; }
        public User User { get => _user; }

        public Profile() { }

        public Profile(User user, string name, DateTime birthDate, string phoneNumber)
        {
            _name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));

            Regex regex = new Regex(phoneMask, RegexOptions.IgnoreCase);
            _phoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? regex.Match(phoneNumber).Success ? regex.Match(phoneNumber).Value : throw new HomeBankingDomainException("Wrong phone number") : String.Empty;

            if (birthDate > DateTime.UtcNow.AddYears(-6) && birthDate <= DateTime.UtcNow.AddYears(-120))
            {
                throw new HomeBankingDomainException(nameof(_birthday));
            }

            _birthday = birthDate;
            _user = user != null ? user : throw new ArgumentNullException(nameof(name));
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(phoneMask, RegexOptions.IgnoreCase);
            _phoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? regex.Match(phoneNumber).Success ? regex.Match(phoneNumber).Value : throw new HomeBankingDomainException("Wrong phone number") : String.Empty;
        }

        public void SetBirthday(DateTime birthday)
        {
            _birthday = birthday;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void ChangeProfile(string name, DateTime birthDay, string phoneNumber)
        {
            Regex regex = new Regex(phoneMask, RegexOptions.IgnoreCase);
            _name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            _birthday = birthDay <= DateTime.UtcNow.AddYears(-6) && birthDay >= DateTime.UtcNow.AddYears(-120) ? birthDay : throw new HomeBankingDomainException(nameof(_birthday));
            _phoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? regex.Match(phoneNumber).Success ? regex.Match(phoneNumber).Value : throw new HomeBankingDomainException("Wrong phone number") : String.Empty;
        }
    }
}