using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorParcelApp.Client.Services {
    public class AuthService : IAuthService {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider) {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<string> GetUsername()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.Name;
        }
        public async Task<bool> IsUserAuthenticated() {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto request) {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto request) {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<string> GetAuthRole()
        {
            await Task.Delay(500);
            var authenticationState = await _authStateProvider.GetAuthenticationStateAsync();
            if (authenticationState.User.Identity.IsAuthenticated)
            {
                string role = authenticationState.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role.Contains("User"))
                {
                   return "User";
                }
                else if (role.Contains("Admin"))
                {
                    return "Admin";
                }
            }
            return string.Empty;
        }
    }
}
