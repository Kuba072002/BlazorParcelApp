using BlazorParcelApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorParcelApp.Server.Services.UserService {
    public class UserService : IUserService {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DataContext _context;

        public UserService(IHttpContextAccessor contextAccessor,DataContext dataContext) {
            _contextAccessor = contextAccessor;
            _context = dataContext;
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
                result = _contextAccessor.HttpContext.User?.Identity?.Name;//.FindFirstValue(ClaimTypes.Name);
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

        public List<string> GetUsernames() => _context.Users.Select(x => x.Username).ToList();
    }
}
