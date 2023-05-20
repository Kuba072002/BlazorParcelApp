namespace BlazorParcelApp.Client.Services {
    public interface IAuthService {
        Task<ServiceResponse<int>> Register(UserRegisterDto request);
        Task<ServiceResponse<string>> Login(UserLoginDto request);
        Task<bool> IsUserAuthenticated();
        Task<string> GetUsername();
    }
}
