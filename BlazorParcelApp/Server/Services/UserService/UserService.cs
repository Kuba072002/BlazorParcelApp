using System.Security.Claims;

namespace BlazorParcelApp.Server.Services.UserService {
    public class UserService : IUserService {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId() {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null) {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return result;
        }

        public string GetUserName() {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null) {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public string GetUserRole() {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null) {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }
    }
}
