using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace ProtelAppT.Service
{
    public class AuthenticationStateService
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public bool IsAuthenticated => _currentUser.Identity.IsAuthenticated;

        public string? GetUserRole()
        {
            if (IsAuthenticated)
            {
                return _currentUser.FindFirst(ClaimTypes.Role)?.Value;
            }
            return null;
        }

        public event Action? AuthenticationStateChanged;

        public void Login(ClaimsPrincipal user)
        {
            _currentUser = user;
            NotifyAuthenticationStateChanged();
        }

        public void Logout()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged();
        }

        private void NotifyAuthenticationStateChanged()
        {
            AuthenticationStateChanged?.Invoke();
        }
    }
}