namespace BlazorParcelApp.Server.Services.UserService {
    public interface IUserService {
        public string GetUserName();
        public string GetUserId();
        public string GetUserRole();
        public Task<List<string>> GetUsernames();
    }
}
