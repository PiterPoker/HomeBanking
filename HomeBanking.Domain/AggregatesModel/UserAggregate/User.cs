using HomeBanking.Domain.Events;
using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.UserAggregate
{
    public class User
      : Entity, IAggregateRoot
    {
        private Profile _profile;
        private DateTime _update;
        private DateTime? _refreshTokenExpiryTime;
        private string _refreshToken;
        private string _password;
        private string _login;

        public string Login { get => _login; }
        public string Password { get => _password; }
        public string RefreshToken { get => _refreshToken; }
        public DateTime? RefreshTokenExpiryTime { get => _refreshTokenExpiryTime; }

        public void SetRefreshTokenExpiryTime(DateTime refreshTokenExpiryTime)
        {
            _refreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public void AddRole(string roleName)
        {
            var role = Role.FromName(roleName);

            if (!_userRoles.Any(ur => ur.Role == role)) 
            {
                _userRoles.Add(new UserRole(this, role));
            }
        }

        public void SetRefreshToken(string refreshToken)
        {
            _refreshToken = !string.IsNullOrWhiteSpace(refreshToken) ? refreshToken : throw new ArgumentNullException($"Element {nameof(refreshToken)} is empty");
        }

        public DateTime Update { get => _update; }
        public Profile Profile { get => _profile; }

        private readonly List<UserRole> _userRoles;
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

        protected User()
        {
            _userRoles = new List<UserRole>();
        }

        public static User NewUser(string login, string password)
        {
            return new User(login, password);
        }

        public void ChangeLogin(string login)
        {
            if (!string.IsNullOrWhiteSpace(login))
                _login = login;
        }

        public User(string login, string password) : this()
        {
            _login = !string.IsNullOrWhiteSpace(login) ? login : throw new ArgumentNullException(nameof(login));
            _password = !string.IsNullOrWhiteSpace(password) ? password : throw new ArgumentNullException(nameof(password));

            _update = DateTime.UtcNow;
            _userRoles.Add(new UserRole(this, Role.User));
        }

        public Profile SetNewProfile(string name, DateTime birthDay, string phoneNumber)
        {

            if (_profile != null)
            {
                _profile.ChangeProfile(name, birthDay, phoneNumber);
                AddDomainEvent(new UserAndProfileVerifiedDomainEvent(_profile));

                return _profile;
            }
            _profile = new Profile(this, name, birthDay, phoneNumber);

            AddDomainEvent(new UserAndProfileVerifiedDomainEvent(_profile));

            return _profile;
        }

        public void ChangePassword(string password)
        {
            _password = !string.IsNullOrWhiteSpace(password) ? password : throw new ArgumentNullException(nameof(password));
            _update = DateTime.UtcNow;
            AddDomainEvent(new PasswordChangedDomainEvent(this));
        }
    }
}
